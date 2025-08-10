using AutoMapper.QueryableExtensions;
using KrosUlohaJH.Helpers;
using KrosUlohaJH.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public async Task<ActionResult> CreateOrUpdateProjekt(Projekt projekt)
        {
            var result = await CreateOrUpdateProjektInternal(projekt);
            return result.response;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkProjekt([FromBody] List<ProjektDto> Projekt)
        {
            return await BulkHelper.PostBulk<ProjektDto, Projekt>(Projekt, CreateOrUpdateProjektInternal);
        }

        private async Task<(bool success, ActionResult response)> CreateOrUpdateProjektInternal(Projekt projekt)
        {
            var (success, response) = await CreateOrUpdate(
                entity: projekt,
                keySelector: p => p.Kod,
                keyValue: projekt.Kod,
                excludedProperties: new[] { "Kod" },
                customValidation: async (p) =>
                {
                    if (!string.IsNullOrWhiteSpace(p.VeduciProjektuRC))
                    {
                        var exists = await _context.Zamestnanci
                            .AnyAsync(z => z.RodneCislo == p.VeduciProjektuRC);
                        if (!exists)
                            return (false, "Rodné číslo neexistuje v tabuľke zamestnanci.");
                    }
                    return (true, null);
                },
                getActionName: nameof(GetProjekt),
                controllerName: "Projekt",
                routeValues: new { kod = projekt.Kod }
            );

            return (success, response);
        }

        [HttpGet("{kod}")]
        public async Task<ActionResult<ProjektDto>> GetProjekt(string kod)
        {
            var projekt = await _context.Projekty
           .Where(d => d.Kod == kod)
           .Include(d => d.Oddelenia)
           .ProjectTo<ZamestnanecDto>(_mapper.ConfigurationProvider)
           .FirstOrDefaultAsync();

            if (projekt == null)
                return NotFound(new { message = "Firma nebola nájdená." });

            return Ok(projekt);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjektDto>>> GetAll()
        {
            return await GetAllEntities<Projekt, ProjektDto>(
                _context.Projekty.Include(d => d.Oddelenia)
            );
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

    public int? DiviziaId { get; set; }

    public string? VeduciProjektuRC { get; set; }

    [JsonPropertyOrder(100)]
    public List<OddeleniaDto>? Oddelenia { get; set; }

}