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

            var existujuci = await _context.Projekty
                .FirstOrDefaultAsync(z => z.Kod == Projekt.Kod);

            if (existujuci != null)
            {

                if (!string.IsNullOrWhiteSpace(Projekt.Kod) &&
                    await _context.Projekty.AnyAsync(u => u.Kod == Projekt.Kod ))
                {
                    return Conflict(new { sprava = "Projekt s tímto kódom už existuje." });
                }

                //aktualizuj hodnoty
                if (!string.IsNullOrWhiteSpace(Projekt.Nazov))
                    existujuci.Nazov = Projekt.Nazov;



                if (!string.IsNullOrWhiteSpace(Projekt.VeduciProjektuRC))
                    existujuci.VeduciProjektuRC = Projekt.VeduciProjektuRC;



                await _context.SaveChangesAsync();
                return Ok(existujuci);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Skontroluj email
            if (await _context.Projekty.AnyAsync(u => u.Kod ==   Projekt.Kod))
            {
                return Conflict(new { sprava = "Projekt s tímto kódom už existuje." });
            }

            _context.Projekty.Add(Projekt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjekt), new { Projekt.Kod }, Projekt);
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