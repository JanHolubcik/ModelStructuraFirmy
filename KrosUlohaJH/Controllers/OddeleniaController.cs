using KrosUlohaJH.Models; // namespace pre tvoje modely
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace KrosUlohaJH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OddeleniaController : ControllerBase
    {
        private readonly StrukturaFirmyContext _context;

        public OddeleniaController(StrukturaFirmyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Divizia>> PostOrUpdateOddelenie(Oddelenie Oddelenie)
        {  
                var (success, result) = await CreateOrUpdate(Oddelenie);
                return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkOddelenia([FromBody] List<Oddelenie> Oddelenia)
        {
            var errors = new List<object>();
            var success = new List<Oddelenie>();

            foreach (var z in Oddelenia)
            {
                var (ok, result) = await CreateOrUpdate(z);

                if (ok && result is ObjectResult r1 && r1.Value is Oddelenie zam)
                    success.Add(zam);
                else
                    errors.Add(new { kod = z.Kod, chyba = (result as ObjectResult)?.Value });
            }

            return Ok(new
            {
                uspesne = success.Count,
                neuspesne = errors.Count,
                chyby = errors
            });
        }

        private async Task<(bool success, ActionResult response)> CreateOrUpdate(Oddelenie Oddelenie)
        {

            bool exists = await _context.Zamestnanci
                 .AnyAsync(z => z.RodneCislo == Oddelenie.VeduciOddeleniaRc);

            if (!exists)
            {
                return (false, BadRequest("Rodné číslo neexistuje v tabuľke zamestnanci."));
            }

            var existujuci = await _context.Oddelenia
                .FirstOrDefaultAsync(z => z.Kod == Oddelenie.Kod);

            if (existujuci != null)
            {



                //aktualizuj hodnoty
                if (!string.IsNullOrWhiteSpace(Oddelenie.Nazov))
                    existujuci.Nazov = Oddelenie.Nazov;



                if (!string.IsNullOrWhiteSpace(Oddelenie.VeduciOddeleniaRc))
                    existujuci.VeduciOddeleniaRc = Oddelenie.VeduciOddeleniaRc;

                if (Oddelenie.ProjektId.HasValue)
                    existujuci.ProjektId = Oddelenie.ProjektId;

                await _context.SaveChangesAsync();
                return  (true, new OkObjectResult(existujuci)); ;
            }


            var contextEdit = new ValidationContext(Oddelenie);
            var resultsEdit = new List<ValidationResult>();
            bool isValidEdit = Validator.TryValidateObject(
                Oddelenie,
                contextEdit,
                resultsEdit,
                validateAllProperties: true
            );

            if (!isValidEdit)
            {
                foreach (var validationResult in resultsEdit)
                {
                    foreach (var memberName in validationResult.MemberNames)
                    {
                        ModelState.AddModelError(memberName, validationResult.ErrorMessage);
                    }
                }

                return (false, new BadRequestObjectResult(ModelState));
            }

            // Skontroluj email
            if (await _context.Oddelenia.AnyAsync(u => u.Kod == Oddelenie.Kod))
            {
                return (false, new ConflictObjectResult(new { sprava = "Toto oddelenie už existuje" }));
            }

            _context.Oddelenia.Add(Oddelenie);
            await _context.SaveChangesAsync();

            return (true, new CreatedAtActionResult(nameof(GetOddelenie), "Oddelenie", new { rc = Oddelenie.Kod }, Oddelenie));
        }

        [HttpGet("{kod}")]
        public async Task<ActionResult<OddeleniaDto>> GetOddelenie(string kod)
        {
            var oddelenie = await _context.Oddelenia
                .Where(d => d.Kod == kod)
                .Include(d => d.Zamestnanci)
                .Select(d => new OddeleniaDto
                {
                    Kod = d.Kod,
                    Nazov = d.Nazov,
                    Zamestnanci = d.Zamestnanci.Select(p => new ZamestnanecDto
                    {
                        Meno = p.Meno,
                        Priezvisko = p.Priezvisko,
                        RodneCislo = p.RodneCislo,
                        Titul = p.Titul,
                    
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (oddelenie == null)
                return NotFound(new { message = "Divízia nebola nájdená." });

            return Ok(oddelenie);
        }

        [HttpDelete("{kod}")]
        public async Task<ActionResult<Oddelenie>> DeleteOddelenie(string Kod)
        {
            var oddelenie = await _context.Oddelenia
                .FirstOrDefaultAsync(z => z.Kod == Kod);

            if (oddelenie == null)
            {
                return NotFound();
            }

            _context.Oddelenia.Remove(oddelenie);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Oddelenie bolo úspešne odstránené." });
        }
    }
}

//slúži na lepšie vrátenie uzla, aj aké iné uzly mu patria
public class OddeleniaDto
{
    public string? Kod { get; set; }
    public string? Nazov { get; set; }
    public List<ZamestnanecDto>? Zamestnanci { get; set; }
}
