using KrosUlohaJH.Models; // namespace pre tvoje modely
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace KrosUlohaJH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiviziaController : ControllerBase
    {
        private readonly StrukturaFirmyContext _context;

        public DiviziaController(StrukturaFirmyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Divizia>> PostOrUpdateDivizia(Divizia Divizia)
        {
            var (success, result) = await CreateOrUpdate(Divizia);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkDivizia([FromBody] List<Divizia> Divizia)
        {
            var errors = new List<object>();
            var success = new List<Divizia>();

            foreach (var z in Divizia)
            {
                var (ok, result) = await CreateOrUpdate(z);

                if (ok && result is ObjectResult r1 && r1.Value is Divizia zam)
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

        private async Task<(bool success, ActionResult response)> CreateOrUpdate(Divizia Divizia)
        {

            var existujuci = await _context.Divizie
                .FirstOrDefaultAsync(z => z.Kod == Divizia.Kod);

            if (existujuci != null)
            {

                if (!string.IsNullOrWhiteSpace(Divizia.Kod) &&
                    await _context.Divizie.AnyAsync(u => u.Kod == Divizia.Kod))
                {

                    return (false, new ConflictObjectResult(new { sprava = "Divizia s tímto kódom už existuje." }));
                }

                //aktualizuj hodnoty
                if (!string.IsNullOrWhiteSpace(Divizia.Nazov))
                    existujuci.Nazov = Divizia.Nazov;



                if (!string.IsNullOrWhiteSpace(Divizia.VeduciRC))
                    existujuci.VeduciRC = Divizia.VeduciRC;



                await _context.SaveChangesAsync();
                return (true, new OkObjectResult(existujuci)); ;
            }


            if (!ModelState.IsValid)
            {
                return (false, new BadRequestObjectResult(ModelState));
            }

            // Skontroluj email
            if (await _context.Divizie.AnyAsync(u => u.Kod == Divizia.Kod))
            {
                return (false, new ConflictObjectResult(new { sprava = "Divizia už existuje" }));
            }

            _context.Divizie.Add(Divizia);
            await _context.SaveChangesAsync();

            return (true, new CreatedAtActionResult(nameof(GetDivizia), "Divizia", new { rc = Divizia.Kod }, Divizia));
        }

        [HttpGet("{kod}")]
        public async Task<ActionResult<DiviziaDto>> GetDivizia(string kod)
        {
            var divizia = await _context.Divizie
                .Where(d => d.Kod == kod)
                .Include(d => d.Projekty)
                .Select(d => new DiviziaDto
                {
                    Kod = d.Kod,
                    Nazov = d.Nazov,
                    Projekty = d.Projekty.Select(p => new ProjektDto
                    {
                        Kod = p.Kod,
                        Nazov = p.Nazov
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (divizia == null)
                return NotFound(new { message = "Divízia nebola nájdená." });

            return Ok(divizia);
        }

        [HttpDelete("{kod}")]
        public async Task<ActionResult<Divizia>> DeleteDivizia(string Kod)
        {
            var divizia = await _context.Divizie
                .FirstOrDefaultAsync(z => z.Kod == Kod);

            if (divizia == null)
            {
                return NotFound();
            }

            _context.Divizie.Remove(divizia);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Divízia bola úspešne odstránená." });
        }
    }
}

//slúži na lepšie vrátenie uzla, aj aké iné uzly mu patria
public class DiviziaDto
{
    public string? Kod { get; set; }
    public string? Nazov { get; set; }
    public List<ProjektDto>? Projekty { get; set; }
}
