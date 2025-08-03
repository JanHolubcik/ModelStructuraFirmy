using AutoMapper;
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
            var (success, result) = await CreateOrUpdate(Firma);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkFirma([FromBody] List<FirmaDto> Firma)
        {
            return await BulkHelper.PostBulk<FirmaDto, Firma>(Firma, CreateOrUpdate);
        }

        private async Task<(bool success, ActionResult response)> CreateOrUpdate(Firma Firma)
        {

            var existujuci = await _context.Firmy
                .FirstOrDefaultAsync(z => z.Kod == Firma.Kod);


            if (!string.IsNullOrWhiteSpace(Firma.RiaditelRc))
            {

                var exists = await _context.Zamestnanci
                    .AnyAsync(z => z.RodneCislo == Firma.RiaditelRc);

                if (!exists)
                    return (false, BadRequest("Rodné číslo neexistuje v tabuľke zamestnanci."));
            }

            var veduciExistuje = await _context.Firmy
            .AnyAsync(d => d.RiaditelRc == Firma.RiaditelRc && d.Kod != Firma.Kod);

            if (veduciExistuje)
            {
                return (false, new ConflictObjectResult(new { sprava = "Riaditeľ nemôže mať viacero firiem." }));
            }

            if (existujuci != null)
            {
                ReplaceValuesOfObject.UpdateNonNullProperties<Firma>(existujuci, Firma, new[] { "Id", "Kod" });
                await _context.SaveChangesAsync();
                return (true, new OkObjectResult(existujuci)); ;
            }


            var (isValid, modelState) = ValidationHelper.ValidateAndHandleModelState(Firma, ModelState);

            if (!isValid)
            {
                return (isValid, new BadRequestObjectResult(modelState));
            }


            _context.Firmy.Add(Firma);
            await _context.SaveChangesAsync();

            return (true, new CreatedAtActionResult(nameof(GetFirma), "Firma", new { kod = Firma.Kod }, Firma));
        }


        [HttpGet("{kod}")]
        public async Task<ActionResult<FirmaDto>> GetFirma(string kod)
        {
            var divizia = await _context.Firmy
                .Where(d => d.Kod == kod)
                .Include(d => d.Divizie)
                .Select(d => new FirmaDto
                {
                    Kod = d.Kod,
                    Nazov = d.Nazov,
                    Divizie = d.Divizie.Select(p => new DiviziaDto
                    {
                        Kod = p.Kod,
                        Nazov = p.Nazov
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (divizia == null)
                return NotFound(new { message = "Firma nebola nájdená." });

            return Ok(divizia);
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
            var firma = await _context.Firmy
                .FirstOrDefaultAsync(z => z.Kod == Kod);

            if (firma == null)
            {
                return NotFound();
            }

            _context.Firmy.Remove(firma);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Firma bola úspešne odstránená." });
        }
    }
}

public class FirmaDto : BaseModel
{
 
    public string? RiaditelRc { get; set; }

    [JsonPropertyOrder(100)]
    public List<DiviziaDto>? Divizie { get; set; }
}

