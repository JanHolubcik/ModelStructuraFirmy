using KrosUlohaJH.Helpers;
using KrosUlohaJH.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace KrosUlohaJH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OddelenieController : ControllerBase
    {
        private readonly StrukturaFirmyContext _context;

        public OddelenieController(StrukturaFirmyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Oddelenie>> PostOrUpdateOddelenie(OddeleniaDto OddelenieDto)
        {

            var mapper = MapperConfig.InitializeAutomapper();
            var Oddelenie = mapper.Map<Oddelenie>(OddelenieDto);
            var (success, result) = await CreateOrUpdate(Oddelenie);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkOddelenia([FromBody] List<OddeleniaDto> Oddelenia)
        {
            var errors = new List<object>();
            var success = new List<Oddelenie>();
            var mapper = MapperConfig.InitializeAutomapper();
          
            foreach (var OddelenieDto in Oddelenia)
            {

                var z = mapper.Map<Oddelenie>(OddelenieDto);
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


            if (!string.IsNullOrWhiteSpace(Oddelenie.VeduciOddeleniaRc))
            {
    
                var exists = await _context.Zamestnanci
                    .AnyAsync(z => z.RodneCislo == Oddelenie.VeduciOddeleniaRc);

                if (!exists)
                    return (false, BadRequest("Rodné číslo neexistuje v tabuľke zamestnanci."));
            }

            var existujuci = await _context.Oddelenia
                .FirstOrDefaultAsync(z => z.Kod == Oddelenie.Kod);

            if (existujuci != null)
            {

                ReplaceValuesOfObject.UpdateNonNullProperties<Oddelenie>(existujuci, Oddelenie, new[] { "Id", "Kod" });

                await _context.SaveChangesAsync();
                return (true, new OkObjectResult(existujuci)); ;
            }


            var (isValid, modelState) = ValidationHelper.ValidateAndHandleModelState(Oddelenie, ModelState);

            if (!isValid)
            {
                return (isValid, new BadRequestObjectResult(modelState));
            }

            // Skontroluj email
            if (await _context.Oddelenia.AnyAsync(u => u.Kod == Oddelenie.Kod))
            {
                return (false, new ConflictObjectResult(new { sprava = "Toto oddelenie už existuje" }));
            }

            _context.Oddelenia.Add(Oddelenie);
            await _context.SaveChangesAsync();

            return (true, new CreatedAtActionResult(nameof(GetOddelenie), "Oddelenie", new { kod = Oddelenie.Kod }, Oddelenie));
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
                    ProjektId = d.ProjektId,
                    VeduciOddeleniaRc = d.VeduciOddeleniaRc,
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

    public int? ProjektId { get; set; }
    public string? VeduciOddeleniaRc { get; set; }
    public List<ZamestnanecDto>? Zamestnanci { get; set; }
}
