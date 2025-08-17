using AutoMapper;
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
    public class FirmaController : BaseApiController
    {
        public FirmaController(StrukturaFirmyContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<ActionResult<Firma>> PostOrUpdateFirma(FirmaDto FirmaDTO)
        { 
            var Firma = _mapper.Map<Firma>(FirmaDTO);
            var (success, result) = await CreateOrUpdateFirmaInternal(Firma);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkFirma([FromBody] List<FirmaDto> Firma)
        {
            return await BulkHelper.PostBulk<FirmaDto, Firma>(Firma, CreateOrUpdateFirmaInternal);
        }

        private async Task<(bool success, ActionResult response)> CreateOrUpdateFirmaInternal(Firma firma)
        {
            var (success, response) = await CreateOrUpdate(
                entity: firma,
                keySelector: p => p.Kod,
                keyValue: firma.Kod,
                excludedProperties: new[] { "Kod" },
                customValidation: async (p) =>
                {
                    if (!string.IsNullOrWhiteSpace(p.RiaditelRc))
                    {
                        var exists = await _context.Zamestnanci
                            .AnyAsync(z => z.RodneCislo == p.RiaditelRc);
                        if (!exists)
                            return (false, "Rodné číslo neexistuje v tabuľke zamestnanci.");
                    }
                    return (true, null);
                },
                getActionName: nameof(GetFirma),
                controllerName: "Firma",
                routeValues: new { kod = firma.Kod }
            );

            return (success, response);
        }


        [HttpGet("{kod}")]
        public async Task<ActionResult<FirmaDto>> GetFirma(string kod)
        {
          return await GetSingleEntity<Firma, FirmaDto>(
              _context.Firmy.Include(d => d.Divizie),
              d => d.Kod == kod
          );
        }

        [HttpGet]
        public async Task<ActionResult<List<FirmaDto>>> GetAll()
        {
            return await GetAllEntities<Firma, FirmaDto>(
                _context.Firmy.Include(d => d.Divizie)
            );
        }

        [HttpDelete("{kod}")]
        public async Task<ActionResult<Firma>> DeleteFirma(string Kod)
        {
            return await DeleteEntity<Firma>(
            _context.Firmy,
            d => d.Kod == Kod
        );
        }
    }
}

//DTO triedy by asi trebalo dat do custom triedy
public class FirmaDto : BaseModel
{
 
    public string? RiaditelRc { get; set; }

    [JsonPropertyOrder(100)]
    public List<DiviziaDto>? Divizie { get; set; }
}

