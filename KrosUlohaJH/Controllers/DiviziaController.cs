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


            var existujuci = await _context.Divizie
                .FirstOrDefaultAsync(z => z.Kod == Divizia.Kod);

            if (existujuci != null)
            {

                if (!string.IsNullOrWhiteSpace(Divizia.Kod) &&
                    await _context.Divizie.AnyAsync(u => u.Kod == Divizia.Kod ))
                {
                    return Conflict(new { sprava = "Divízia s tímto kódom už existuje." });
                }

                //aktualizuj hodnoty
                if (!string.IsNullOrWhiteSpace(Divizia.Nazov))
                    existujuci.Nazov = Divizia.Nazov;



                if (!string.IsNullOrWhiteSpace(Divizia.VeduciRC))
                    existujuci.VeduciRC = Divizia.VeduciRC;



                await _context.SaveChangesAsync();
                return Ok(existujuci);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Skontroluj email
            if (await _context.Divizie.AnyAsync(u => u.Kod == Divizia.Kod))
            {
                return Conflict(new { sprava = "Tento email už je zaregistrovaný." });
            }

            _context.Divizie.Add(Divizia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDivizia), new { Divizia.Kod }, Divizia);
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
