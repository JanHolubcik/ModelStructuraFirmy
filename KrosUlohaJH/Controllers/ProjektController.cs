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
    public class ProjektController : ControllerBase
    {
        private readonly StrukturaFirmyContext _context;

        public ProjektController(StrukturaFirmyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Projekt>> PostOrUpdateProjekt(ProjektDto ProjektDTO)
        {
            Projekt Projekt = new Projekt
            {
                Kod = ProjektDTO.Kod,
                Nazov = ProjektDTO.Nazov,
                DiviziaId = ProjektDTO.DiviziaId,
                VeduciProjektuRC = ProjektDTO.VeduciProjektuRC,
            };
            var (success, result) = await CreateOrUpdate(Projekt);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkProjekt([FromBody] List<ProjektDto> Projekt)
        {
            var errors = new List<object>();
            var success = new List<Projekt>();

            foreach (var projektDto in Projekt)
            {

                Projekt z = new Projekt
                {
                    Kod = projektDto.Kod,
                    Nazov = projektDto.Nazov,
                    DiviziaId = projektDto.DiviziaId,
                    VeduciProjektuRC = projektDto.VeduciProjektuRC,
                };
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
            if (!string.IsNullOrWhiteSpace(Projekt.VeduciProjektuRC))
            {
                // If provided, check if it exists in Zamestnanci
                var exists = await _context.Zamestnanci
                    .AnyAsync(z => z.RodneCislo == Projekt.VeduciProjektuRC);

                if (!exists)
                    return (false, BadRequest("Rodné číslo neexistuje v tabuľke zamestnanci."));
            }


            var existujuci = await _context.Projekty
                .FirstOrDefaultAsync(z => z.Kod == Projekt.Kod);

            if (existujuci != null)
            {


                //aktualizuj hodnoty
                if (!string.IsNullOrWhiteSpace(Projekt.Nazov))
                    existujuci.Nazov = Projekt.Nazov;



                if (!string.IsNullOrWhiteSpace(Projekt.VeduciProjektuRC))
                    existujuci.VeduciProjektuRC = Projekt.VeduciProjektuRC;

                if (Projekt.DiviziaId.HasValue)
                    existujuci.DiviziaId = Projekt.DiviziaId;


                await _context.SaveChangesAsync();
                return (true, new OkObjectResult(existujuci)); ;
            }


            var contextEdit = new ValidationContext(Projekt);
            var resultsEdit = new List<ValidationResult>();
            bool isValidEdit = Validator.TryValidateObject(
                Projekt,
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
            if (await _context.Projekty.AnyAsync(u => u.Kod == Projekt.Kod))
            {
                return (false, new ConflictObjectResult(new { sprava = "Projekt už existuje" }));
            }

            _context.Projekty.Add(Projekt);
            await _context.SaveChangesAsync();

            return (true, new CreatedAtActionResult(nameof(GetProjekt), "Projekt", new { kod = Projekt.Kod }, Projekt));
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

    public int? DiviziaId { get; set; }

    public string? VeduciProjektuRC { get; set; }

}