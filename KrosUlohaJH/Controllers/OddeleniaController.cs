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


            var existujuci = await _context.Divizie
                .FirstOrDefaultAsync(z => z.Kod == Oddelenie.Kod);

            if (existujuci != null)
            {

                if (!string.IsNullOrWhiteSpace(Oddelenie.Kod) &&
                    await _context.Divizie.AnyAsync(u => u.Kod == Oddelenie.Kod ))
                {
                    return Conflict(new { sprava = "Divízia s tímto kódom už existuje." });
                }

                //aktualizuj hodnoty
                if (!string.IsNullOrWhiteSpace(Oddelenie.Nazov))
                    existujuci.Nazov = Oddelenie.Nazov;



                if (!string.IsNullOrWhiteSpace(Oddelenie.VeduciOddeleniaRc))
                    existujuci.VeduciRC = Oddelenie.VeduciOddeleniaRc;



                await _context.SaveChangesAsync();
                return Ok(existujuci);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Skontroluj email
            if (await _context.Divizie.AnyAsync(u => u.Kod == Oddelenie .Kod))
            {
                return Conflict(new { sprava = "Tento email už je zaregistrovaný." });
            }

            _context.Oddelenia.Add(Oddelenie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOddelenie), new { Oddelenie.Kod }, Oddelenie);
        }

        [HttpGet("{kod}")]
        public async Task<ActionResult<OddeleniaDto>> GetOddelenie(string kod)
        {
            var divizia = await _context.Oddelenia
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
                        RC   = p.RodneCislo,
                        Titul = p.Titul,
                    
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
public class OddeleniaDto
{
    public string? Kod { get; set; }
    public string? Nazov { get; set; }
    public List<ZamestnanecDto>? Zamestnanci { get; set; }
}
