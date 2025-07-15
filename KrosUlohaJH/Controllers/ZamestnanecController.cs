using KrosUlohaJH.Models; // namespace pre tvoje modely
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace KrosUlohaJH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZamestnanecController : ControllerBase
    {
        private readonly StrukturaFirmyContext _context;

        public ZamestnanecController(StrukturaFirmyContext context)
        {
            _context = context;
        }

        // post, posielam zamestnanca aby sa vytvoril na DB alebo zeditoval
        [HttpPost]
        public async Task<ActionResult<Zamestnanec>> PostOrUpdateZamestnanec(Zamestnanec zamestnanec)
        {
            var (success, result) = await CreateOrUpdate(zamestnanec);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkZamestnanci([FromBody] List<Zamestnanec> zamestnanci)
        {
            var errors = new List<object>();
            var success = new List<Zamestnanec>();

            foreach (var z in zamestnanci)
            {
                var (ok, result) = await CreateOrUpdate(z);

                if (ok && result is ObjectResult r1 && r1.Value is Zamestnanec zam)
                    success.Add(zam);
                else
                    errors.Add(new { rodneCislo = z.RodneCislo, chyba = (result as ObjectResult)?.Value });
            }

            return Ok(new
            {
                uspesne = success.Count,
                neuspesne = errors.Count,
                chyby = errors
            });
        }

        //Funcionalitu som rozdelil aby som mohol vytvoriť bulk na začiatku kvôli testom.
        private async Task<(bool success, ActionResult response)> CreateOrUpdate(Zamestnanec zamestnanec)
        {
            if (string.IsNullOrWhiteSpace(zamestnanec.RodneCislo))
            {
                return (false, new BadRequestObjectResult(new { sprava = "Rodné číslo musí byť vyplnené." }));
            }

            var context = new ValidationContext(zamestnanec) { MemberName = nameof(Zamestnanec.RodneCislo) };
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(zamestnanec.RodneCislo, context, results);

            if (!isValid)
            {
                var chyba = results.First().ErrorMessage;
                return (false, new BadRequestObjectResult(new { sprava = chyba }));
            }

            var existujuci = await _context.Zamestnanci
                .FirstOrDefaultAsync(z => z.RodneCislo == zamestnanec.RodneCislo);

            if (existujuci != null)
            {
                if (!string.IsNullOrWhiteSpace(zamestnanec.Email) &&
                    await _context.Zamestnanci.AnyAsync(u => u.Email == zamestnanec.Email && u.RodneCislo != zamestnanec.RodneCislo))
                {
                    return (false, new ConflictObjectResult(new { sprava = "Tento email už je zaregistrovaný." }));
                }

                // aktualizuj hodnoty
                if (!string.IsNullOrWhiteSpace(zamestnanec.Meno))
                    existujuci.Meno = zamestnanec.Meno;
                if (!string.IsNullOrWhiteSpace(zamestnanec.Priezvisko))
                    existujuci.Priezvisko = zamestnanec.Priezvisko;
                if (!string.IsNullOrWhiteSpace(zamestnanec.Email))
                    existujuci.Email = zamestnanec.Email;
                if (!string.IsNullOrWhiteSpace(zamestnanec.Titul))
                    existujuci.Titul = zamestnanec.Titul;
                if (zamestnanec.OddelenieId.HasValue)
                    existujuci.OddelenieId = zamestnanec.OddelenieId;

                await _context.SaveChangesAsync();
                return (true, new OkObjectResult(existujuci));
            }

            if (!ModelState.IsValid)
                return (false, new BadRequestObjectResult(ModelState));

            if (await _context.Zamestnanci.AnyAsync(u => u.Email == zamestnanec.Email))
                return (false, new ConflictObjectResult(new { sprava = "Tento email už je zaregistrovaný." }));

            _context.Zamestnanci.Add(zamestnanec);
            await _context.SaveChangesAsync();
            return (true, new CreatedAtActionResult(nameof(GetZamestnanec), "Zamestnanec", new { rc = zamestnanec.RodneCislo }, zamestnanec));
        }

        //api/Zamestnanec/{rc}
        [HttpGet]
        public async Task<IActionResult> GetZamestnanec([FromQuery] string rc)
        {
            var zamestnanec = await _context.Zamestnanci
                .FirstOrDefaultAsync(z => z.RodneCislo == rc);

            if (zamestnanec == null)
            {
                return NotFound();
            }

            return Ok(zamestnanec);
        }

        [HttpGet("{rc}")]
        public async Task<ActionResult<Zamestnanec>> DeleteZamestnanec(string rc)
        {
            var zamestnanec = await _context.Zamestnanci
                .FirstOrDefaultAsync(z => z.RodneCislo == rc);

            if (zamestnanec == null)
            {
                return NotFound();
            }

            _context.Zamestnanci.Remove(zamestnanec);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Zamestnanec bol úspešne odstránený." });
        }

    }
}

public class ZamestnanecDto
{
    public string? RC { get; set; }
    public string? Meno { get; set; }
    public string? Priezvisko { get; set; }
    public string Titul { get; set; } = string.Empty;
}