using Microsoft.AspNetCore.Mvc;
using KrosUlohaJH.Models; // namespace pre tvoje modely
using Microsoft.EntityFrameworkCore;

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

        // post, posielam zamestnanca aby sa vytvoril na DB
        [HttpPost]
        public async Task<ActionResult<Zamestnanec>> PostZamestnanec(Zamestnanec zamestnanec)
        {
            //skontrolujem či je valídny zamestnanec
            if (!ModelState.IsValid)
            {
               
                return BadRequest(ModelState);
            }
            // ak tam už je zamestnanec s tímto rodným číslom alebo mailom
            if (await _context.Zamestnanci.AnyAsync(z => z.RodneCislo == zamestnanec.RodneCislo))
            {
                return Conflict("Zamestnanec s týmto rodným číslom už existuje.");
            }
            if (await _context.Zamestnanci.AnyAsync(u => u.Email == zamestnanec.Email))
            {
                return Conflict(new { sprava = "Tento email už je zaregistrovaný." });
            }

            _context.Zamestnanci.Add(zamestnanec);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetZamestnanec), new { rc = zamestnanec.RodneCislo }, zamestnanec);
        }

        //api/Zamestnanec/{rc}
        [HttpGet("{rc}")]
        public async Task<ActionResult<Zamestnanec>> GetZamestnanec(string rc)
        {
            var zamestnanec = await _context.Zamestnanci
                .FirstOrDefaultAsync(z => z.RodneCislo == rc);

            if (zamestnanec == null)
            {
                return NotFound();
            }

            return zamestnanec;
        }
    }
}
