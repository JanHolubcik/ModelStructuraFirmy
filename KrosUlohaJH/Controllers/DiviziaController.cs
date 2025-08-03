using KrosUlohaJH.Helpers;
using KrosUlohaJH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KrosUlohaJH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiviziaController :  BaseApiController
    {
   
        public DiviziaController(StrukturaFirmyContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<ActionResult<Divizia>> PostOrUpdateDivizia(DiviziaDto DiviziaDto)
        {


            var mapper = MapperConfig.InitializeAutomapper();
            var Divizia = mapper.Map<Divizia>(DiviziaDto);
            var (success, result) = await CreateOrUpdate(Divizia);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkDivizia([FromBody] List<DiviziaDto> Divizia)
        {
            return await BulkHelper.PostBulk<DiviziaDto, Divizia>(Divizia, CreateOrUpdate);
        }

        private async Task<(bool success, ActionResult response)> CreateOrUpdate(Divizia Divizia)
        {


            if (!string.IsNullOrWhiteSpace(Divizia.VeduciRC))
            {
 
                var exists = await _context.Zamestnanci
                    .AnyAsync(z => z.RodneCislo == Divizia.VeduciRC);

                if (!exists)
                    return (false, BadRequest("Rodné číslo neexistuje v tabuľke zamestnanci."));
            }

            var existujuci = await _context.Divizie
                .FirstOrDefaultAsync(z => z.Kod == Divizia.Kod);


            if (existujuci != null)
            {

                ReplaceValuesOfObject.UpdateNonNullProperties<Divizia>(existujuci, Divizia, new[] { "Id", "Kod" });

                await _context.SaveChangesAsync();
                return (true, new OkObjectResult(existujuci)); ;
            }


            var (isValid, modelState) = ValidationHelper.ValidateAndHandleModelState(Divizia, ModelState);

            if (!isValid)
            {
                return (isValid, new BadRequestObjectResult(modelState));
            }

            // Skontroluj email
            if (await _context.Divizie.AnyAsync(u => u.Kod == Divizia.Kod))
            {
                return (false, new ConflictObjectResult(new { sprava = "Divizia už existuje" }));
            }



            _context.Divizie.Add(Divizia);
            await _context.SaveChangesAsync();

            return (true, new CreatedAtActionResult(nameof(GetDivizia), "Divizia", new { kod = Divizia.Kod }, Divizia));
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

        [HttpGet]
        public async Task<ActionResult<List<DiviziaDto>>> GetAll()
        {
            return await GetAllEntities<Divizia, DiviziaDto>(
                _context.Divizie.Include(d => d.Projekty)
            );
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
public class DiviziaDto : BaseModel
{

   

    public int? FirmaId { get; set; }

    public string? VeduciRC { get; set; }
   
    [JsonPropertyOrder(100)]
    public List<ProjektDto>? Projekty { get; set; }

}
