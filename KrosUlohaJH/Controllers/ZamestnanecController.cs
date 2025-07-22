using KrosUlohaJH.Helpers;
using KrosUlohaJH.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Xunit.Sdk;

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
        public async Task<ActionResult<Zamestnanec>> PostOrUpdateZamestnanec(ZamestnanecDto zamestnanecDTO)
        {
            var zamestnanec = new Zamestnanec
            {
                RodneCislo = zamestnanecDTO.RodneCislo,
                Titul = zamestnanecDTO.Titul,
                OddelenieId = zamestnanecDTO.OddelenieId,
                Email = zamestnanecDTO.Email,
                Meno = zamestnanecDTO.Meno,
                Priezvisko = zamestnanecDTO.Priezvisko,
                TelefonneCislo = zamestnanecDTO.TelefonneCislo,

            };
            var (success, result) = await CreateOrUpdate(zamestnanec);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkZamestnanci([FromBody] List<ZamestnanecDto> zamestnanci)
        {
            var errors = new List<object>();
            var success = new List<Zamestnanec>();

            foreach (var zamestnanecDTO in zamestnanci)
            {
                var zamestnanec = new Zamestnanec
                {
                    RodneCislo = zamestnanecDTO.RodneCislo,
                    Titul = zamestnanecDTO.Titul,
                    OddelenieId = zamestnanecDTO.OddelenieId,
                    Email = zamestnanecDTO.Email,
                    Meno = zamestnanecDTO.Meno,
                    Priezvisko = zamestnanecDTO.Priezvisko,
                    TelefonneCislo = zamestnanecDTO.TelefonneCislo,

                };
                var (ok, result) = await CreateOrUpdate(zamestnanec);

                if (ok && result is ObjectResult r1 && r1.Value is Zamestnanec zam)
                    success.Add(zam);
                else
                    errors.Add(new { rodneCislo = zamestnanec.RodneCislo, chyba = (result as ObjectResult)?.Value });
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
            bool isValidRC = Validator.TryValidateProperty(zamestnanec.RodneCislo, context, results);

            if (!isValidRC)
            {
                var chyba = results.First().ErrorMessage;
                return (false, new BadRequestObjectResult(new { sprava = chyba }));
            }

            if (!string.IsNullOrWhiteSpace(zamestnanec.TelefonneCislo))
            {

                if (!Regex.IsMatch(zamestnanec.TelefonneCislo, @"^\+[1-9]\d{1,14}$"))
                {
                    return (false, new BadRequestObjectResult(new { sprava = "Telefónne číslo je v zlom formáte (medzinárodný formát)" }));
                }

                var existujeTelefon = await _context.Zamestnanci
                     .AnyAsync(z => z.TelefonneCislo == zamestnanec.TelefonneCislo && z.RodneCislo != zamestnanec.RodneCislo);
                if (existujeTelefon)
                {
                    return (false, new ConflictObjectResult(new { sprava = "Telefónne čislo je už zaregistrované." }));
                }
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
                ReplaceValuesOfObject.UpdateNonNullProperties<Zamestnanec>(existujuci, zamestnanec, new[] { "RodneCislo" });
                await _context.SaveChangesAsync();
                return (true, new OkObjectResult(existujuci));
            }

            var (isValid, modelState) = ValidationHelper.ValidateAndHandleModelState(zamestnanec, ModelState);

            if (!isValid)
            {
                return (isValid, new BadRequestObjectResult(modelState));
            }

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

        [HttpDelete]
        public async Task<ActionResult<Zamestnanec>> DeleteZamestnanec([FromQuery] string rc)
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

    public string? RodneCislo { get; set; }

    public string? Meno { get; set; }

    public string? Priezvisko { get; set; }

    public string? Email { get; set; }
    public string? Titul { get; set; }
    public string? TelefonneCislo { get; set; }
    public int? OddelenieId { get; set; }

}