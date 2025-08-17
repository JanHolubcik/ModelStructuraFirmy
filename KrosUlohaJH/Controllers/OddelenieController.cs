using AutoMapper.QueryableExtensions;
using KrosUlohaJH.Helpers;
using KrosUlohaJH.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace KrosUlohaJH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OddelenieController : BaseApiController
    {
        public OddelenieController(StrukturaFirmyContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<ActionResult<Oddelenie>> PostOrUpdateOddelenie(OddeleniaDto OddelenieDto)
        {

            var mapper = MapperConfig.InitializeAutomapper();
            var Oddelenie = mapper.Map<Oddelenie>(OddelenieDto);
            var (success, result) = await CreateOrUpdateOddelenieInternal(Oddelenie);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkOddelenia([FromBody] List<OddeleniaDto> Oddelenia)
        {
            return await BulkHelper.PostBulk<OddeleniaDto, Oddelenie>(Oddelenia, CreateOrUpdateOddelenieInternal);
        }

        private async Task<(bool success, ActionResult response)> CreateOrUpdateOddelenieInternal(Oddelenie oddelenie)
        {
            var (success, response) = await CreateOrUpdate(
                entity: oddelenie,
                keySelector: p => p.Kod,
                keyValue: oddelenie.Kod,
                excludedProperties: new[] { "Kod" },
                customValidation: async (p) =>
                {
                    if (!string.IsNullOrWhiteSpace(p.VeduciOddeleniaRc))
                    {
                        var exists = await _context.Zamestnanci
                            .AnyAsync(z => z.RodneCislo == p.VeduciOddeleniaRc);
                        if (!exists)
                            return (false, "Rodné číslo neexistuje v tabuľke zamestnanci.");
                    }
                    return (true, null);
                },
                getActionName: nameof(GetOddelenie),
                controllerName: "Oddelenie",
                routeValues: new { kod = oddelenie.Kod }
            );

            return (success, response);
        }
        [HttpGet]
        public async Task<ActionResult<List<OddeleniaDto>>> GetAll()
        {
            return await GetAllEntities<Oddelenie, OddeleniaDto>(
                _context.Oddelenia.Include(d => d.Zamestnanci)
            );
        }
        //tieto 2 by mohli byt aj jedna funkcia ?
        [HttpGet("{kod}")]
        public async Task<ActionResult<OddeleniaDto>> GetOddelenie(string kod)
        {
            return await GetSingleEntity<Oddelenie, OddeleniaDto>(
             _context.Oddelenia.Include(d => d.Zamestnanci),
             d => d.Kod == kod
         );
        }

        [HttpDelete("{kod}")]
        public async Task<ActionResult<Oddelenie>> DeleteOddelenie(string Kod)
        {
            return await DeleteEntityByProperty<Oddelenie, string>(
                 _context.Oddelenia,
                 d => d.Kod,
                 Kod,
                 "Oddelenie"
             );
        }
    }
}

//slúži na lepšie vrátenie uzla, aj aké iné uzly mu patria
public class OddeleniaDto : BaseModel
{


    public int? ProjektId { get; set; }
    public string? VeduciOddeleniaRc { get; set; }
    public List<ZamestnanecDto>? Zamestnanci { get; set; }
}
