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
    public class OddelenieController : BaseApiController
    {
        public OddelenieController(StrukturaFirmyContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<ActionResult<Oddelenie>> PostOrUpdateOddelenie(OddeleniaDto OddelenieDto)
        {

            var mapper = MapperConfig.InitializeAutomapper();
            var Oddelenie = mapper.Map<Oddelenie>(OddelenieDto);
            var (success, result) = await CreateOrUpdateOddelenieInternal(Oddelenie);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkOddelenia([FromBody] List<OddeleniaDto> Oddelenia)
        {
            return await BulkHelper.PostBulk<OddeleniaDto, Oddelenie>(Oddelenia, CreateOrUpdateOddelenieInternal);
        }

        private async Task<(bool success, ActionResult response)> CreateOrUpdateOddelenieInternal(Oddelenie oddelenie)
        {
            var (success, response) = await CreateOrUpdate(
                entity: oddelenie,
                keySelector: p => p.Kod,
                keyValue: oddelenie.Kod,
                excludedProperties: new[] { "Kod" },
                customValidation: async (p) =>
                {
                    if (!string.IsNullOrWhiteSpace(p.VeduciOddeleniaRc))
                    {
                        var exists = await _context.Zamestnanci
                            .AnyAsync(z => z.RodneCislo == p.VeduciOddeleniaRc);
                        if (!exists)
                            return (false, "Rodné číslo neexistuje v tabuľke zamestnanci.");
                    }
                    return (true, null);
                },
                getActionName: nameof(GetOddelenie),
                controllerName: "Projekt",
                routeValues: new { kod = oddelenie.Kod }
            );

            return (success, response);
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
public class OddeleniaDto : BaseModel
{


    public int? ProjektId { get; set; }
    public string? VeduciOddeleniaRc { get; set; }
    public List<ZamestnanecDto>? Zamestnanci { get; set; }
}
