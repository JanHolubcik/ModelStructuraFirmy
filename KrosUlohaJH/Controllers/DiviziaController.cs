using AutoMapper.QueryableExtensions;
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
    public class DiviziaController : BaseApiController
    {

        public DiviziaController(StrukturaFirmyContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<ActionResult<Divizia>> PostOrUpdateDivizia(DiviziaDto DiviziaDto)
        {


            var mapper = MapperConfig.InitializeAutomapper();
            var Divizia = mapper.Map<Divizia>(DiviziaDto);
            var (success, result) = await CreateOrUpdateDiviziaInternal(Divizia);
            return result;
        }
        private async Task<(bool success, ActionResult response)> CreateOrUpdateDiviziaInternal(Divizia divizia)
        {
            var (success, response) = await CreateOrUpdate(
                entity: divizia,
                keySelector: p => p.Kod,
                keyValue: divizia.Kod,
                excludedProperties: new[] { "Kod" },
                customValidation: async (p) =>
                {
                    if (!string.IsNullOrWhiteSpace(p.VeduciRC))
                    {
                        var exists = await _context.Zamestnanci
                            .AnyAsync(z => z.RodneCislo == p.VeduciRC);
                        if (!exists)
                            return (false, "Rodné číslo neexistuje v tabuľke zamestnanci.");
                    }
                    return (true, null);
                },
                getActionName: nameof(GetDivizia),
                controllerName: "Divizia",
                routeValues: new { kod = divizia.Kod }
            );

            return (success, response);
        }

        [HttpGet("{kod}")]
        public async Task<ActionResult<DiviziaDto>> GetDivizia(string kod)
        {
            return await GetSingleEntity<Divizia, DiviziaDto>(
                _context.Divizie.Include(d => d.Projekty),
                d => d.Kod == kod
            );
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
            return await DeleteEntity<Divizia>(
                 _context.Divizie,
                 d => d.Kod == Kod
             );
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

