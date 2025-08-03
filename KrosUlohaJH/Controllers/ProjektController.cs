using KrosUlohaJH.Helpers;
using KrosUlohaJH.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrosUlohaJH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjektController : BaseApiController
    {
        public ProjektController(StrukturaFirmyContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<ActionResult<Projekt>> PostOrUpdateProjekt(ProjektDto ProjektDTO)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var Projekt = mapper.Map<Projekt>(ProjektDTO);
            var (success, result) = await CreateOrUpdate(Projekt);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkProjekt([FromBody] List<ProjektDto> Projekt)
        {
            return await BulkHelper.PostBulk<ProjektDto, Projekt>(Projekt, CreateOrUpdate);
        }

        private async Task<(bool success, ActionResult response)> CreateOrUpdate(Projekt Projekt)
        {
            if (!string.IsNullOrWhiteSpace(Projekt.VeduciProjektuRC))
            {

                var exists = await _context.Zamestnanci
                    .AnyAsync(z => z.RodneCislo == Projekt.VeduciProjektuRC);

                if (!exists)
                    return (false, BadRequest("Rodné číslo neexistuje v tabuľke zamestnanci."));
            }


            var existujuci = await _context.Projekty
                .FirstOrDefaultAsync(z => z.Kod == Projekt.Kod);

            if (existujuci != null)
            {

                ReplaceValuesOfObject.UpdateNonNullProperties<Projekt>(existujuci, Projekt, new[] { "Id", "Kod" });
                await _context.SaveChangesAsync();
                return (true, new OkObjectResult(existujuci)); ;
            }


            var (isValid, modelState) = ValidationHelper.ValidateAndHandleModelState(Projekt, ModelState);

            if (!isValid)
            {
                return (isValid, new BadRequestObjectResult(modelState));
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
                    Oddelenia = d.Oddelenia.Select(p => new OddeleniaDto
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
        public async Task<ActionResult<Projekt>> DeleteProjekt(string Kod)
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


public class ProjektDto : BaseModel
{

    public List<OddeleniaDto>? Oddelenia { get; set; }

    public int? DiviziaId { get; set; }

    public string? VeduciProjektuRC { get; set; }

}