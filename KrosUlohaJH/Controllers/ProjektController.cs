using KrosUlohaJH.Models; // namespace pre tvoje modely
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace KrosUlohaJH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjektController : ControllerBase
    {
        private readonly StrukturaFirmyContext _context;

        public ProjektController(StrukturaFirmyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Projekt>> PostOrUpdateProjekt(Projekt Projekt)
        {
            var (success, result) = await CreateOrUpdate(Projekt);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkProjekt([FromBody] List<Projekt> Projekt)
        {
            var errors = new List<object>();
            var success = new List<Projekt>();

            foreach (var z in Projekt)
            {
                var (ok, result) = await CreateOrUpdate(z);

                if (ok && result is ObjectResult r1 && r1.Value is Projekt zam)
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

        private async Task<(bool success, ActionResult response)> CreateOrUpdate(Projekt Projekt)
        {

            var existujuci = await _context.Projekty
                .FirstOrDefaultAsync(z => z.Kod == Projekt.Kod);

            if (existujuci != null)
            {

                if (!string.IsNullOrWhiteSpace(Projekt.Kod) &&
                    await _context.Projekty.AnyAsync(u => u.Kod == Projekt.Kod))
                {

                    return (false, new ConflictObjectResult(new { sprava = "Projekt s tímto kódom už existuje." }));
                }

                //aktualizuj hodnoty
                if (!string.IsNullOrWhiteSpace(Projekt.Nazov))
                    existujuci.Nazov = Projekt.Nazov;



                if (!string.IsNullOrWhiteSpace(Projekt.VeduciProjektuRC))
                    existujuci.VeduciProjektuRC = Projekt.VeduciProjektuRC;



                await _context.SaveChangesAsync();
                return (true, new OkObjectResult(existujuci)); ;
            }


            if (!ModelState.IsValid)
            {
                return (false, new BadRequestObjectResult(ModelState));
            }

            // Skontroluj email
            if (await _context.Projekty.AnyAsync(u => u.Kod == Projekt.Kod))
            {
                return (false, new ConflictObjectResult(new { sprava = "Projekt už existuje" }));
            }

            _context.Projekty.Add(Projekt);
            await _context.SaveChangesAsync();

            return (true, new CreatedAtActionResult(nameof(GetProjekt), "Projekt", new { rc = Projekt.Kod }, Projekt));
        }

        [HttpGet("{kod}")]
        public async Task<ActionResult<ProjektDto>> GetProjekt(string kod)
        {
            var projekt = await _context.Projekty
                .Where(d => d.Kod == kod)
                .Include(d => d.Oddelenia)
                .Select(d => new ProjektDto
                {
                    Kod = d.Kod,
                    Nazov = d.Nazov,
                    Projekty = d.Oddelenia.Select(p => new OddeleniaDto
                    {
                        Kod = p.Kod,
                        Nazov = p.Nazov
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (projekt == null)
                return NotFound(new { message = "Projekt nebol nájdení." });

            return Ok(projekt);
        }

        [HttpDelete("{kod}")]
        public async Task<ActionResult< Projekt>> DeleteProjekt(string Kod)
        {
            var projekt = await _context.Projekty
                .FirstOrDefaultAsync(z => z.Kod == Kod);

            if (projekt == null)
            {
                return NotFound();
            }

            _context.Projekty.Remove(projekt);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Projekt bol úspešne odstránení." });
        }
    }
}


public class ProjektDto
{
    public string? Kod { get; set; }
    public string? Nazov { get; set; }
    public List<OddeleniaDto>? Projekty { get; set; }
}