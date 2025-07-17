using KrosUlohaJH.Models; // namespace pre tvoje modely
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrosUlohaJH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirmaController : ControllerBase
    {
        private readonly StrukturaFirmyContext _context;

        public FirmaController(StrukturaFirmyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Firma>> PostOrUpdateFirma(FirmaDto FirmaDTO)
        {
            var Firma = new Firma
            {
                Kod = FirmaDTO.Kod,
                Nazov = FirmaDTO.Nazov,
                RiaditelRc = FirmaDTO.RiaditelRc,              

            };
            var (success, result) = await CreateOrUpdate(Firma);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkFirma([FromBody] List<FirmaDto> FirmaDTO)
        {
            var errors = new List<object>();
            var success = new List<Firma>();

            foreach (var firma in FirmaDTO)
            {
                var z = new Firma
                {
                    Kod = firma.Kod,
                    Nazov = firma.Nazov,
                    RiaditelRc = firma.RiaditelRc,

                };
                var (ok, result) = await CreateOrUpdate(z);

                if (ok && result is ObjectResult r1 && r1.Value is Firma zam)
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

        private async Task<(bool success, ActionResult response)> CreateOrUpdate(Firma Firma)
        {

            var existujuci = await _context.Firmy
                .FirstOrDefaultAsync(z => z.Kod == Firma.Kod);

            if (existujuci != null)
            {



                //aktualizuj hodnoty
                if (!string.IsNullOrWhiteSpace(Firma.Nazov))
                    existujuci.Nazov = Firma.Nazov;



                if (!string.IsNullOrWhiteSpace(Firma.RiaditelRc))
                    existujuci.RiaditelRc = Firma.RiaditelRc;



                await _context.SaveChangesAsync();
                return (true, new OkObjectResult(existujuci)); ;
            }


            var contextEdit = new ValidationContext(Firma);
            var resultsEdit = new List<ValidationResult>();
            bool isValidEdit = Validator.TryValidateObject(
                Firma,
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
            if (await _context.Firmy.AnyAsync(u => u.Kod == Firma.Kod))
            {
                return (false, new ConflictObjectResult(new { sprava = "Firma už existuje" }));
            }
            var veduciExistuje = await _context.Firmy
    .AnyAsync(d => d.RiaditelRc == Firma.RiaditelRc && d.Kod != Firma.Kod);

            if(veduciExistuje)
            {
                return (false, new ConflictObjectResult(new { sprava = "Riaditeľ nemôže mať viacero firiem." }));
            }
            _context.Firmy.Add(Firma);
            await _context.SaveChangesAsync();

            return (true, new CreatedAtActionResult(nameof(GetFirma), "Firma", new { kod = Firma.Kod }, Firma));
        }


        [HttpGet("{kod}")]
        public async Task<ActionResult<FirmaDto>> GetFirma(string kod)
        {
            var divizia = await _context.Firmy
                .Where(d => d.Kod == kod)
                .Include(d => d.Divizie)
                .Select(d => new FirmaDto
                {
                    Kod = d.Kod,
                    Nazov = d.Nazov,
                    Divizie = d.Divizie.Select(p => new DiviziaDto
                    {
                        Kod = p.Kod,
                        Nazov = p.Nazov
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (divizia == null)
                return NotFound(new { message = "Firma nebola nájdená." });

            return Ok(divizia);
        }

        [HttpDelete("{kod}")]
        public async Task<ActionResult<Firma>> DeleteFirma(string Kod)
        {
            var firma = await _context.Firmy
                .FirstOrDefaultAsync(z => z.Kod == Kod);

            if (firma == null)
            {
                return NotFound();
            }

            _context.Firmy.Remove(firma);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Firma bola úspešne odstránená." });
        }
    }
}

public class FirmaDto
{
    public string? Kod { get; set; }
    public string? Nazov { get; set; }
    public List<DiviziaDto>? Divizie { get; set; }

    public string? RiaditelRc { get; set; }  // FK na Zamestnanec.RodneCislo


}

