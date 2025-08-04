using KrosUlohaJH.Helpers;
using KrosUlohaJH.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Xunit.Sdk;

namespace KrosUlohaJH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZamestnanecController : BaseApiController
    {
        public ZamestnanecController(StrukturaFirmyContext context) : base(context)
        {
        }


        // post, posielam zamestnanca aby sa vytvoril na DB alebo zeditoval
        [HttpPost]
        public async Task<ActionResult<Zamestnanec>> PostOrUpdateZamestnanec(ZamestnanecDto zamestnanecDTO)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var zamestnanec = mapper.Map<Zamestnanec>(zamestnanecDTO);
            var (success, result) = await CreateOrUpdateZamestnanecInternal(zamestnanec);
            return result;
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> PostBulkZamestnanci([FromBody] List<ZamestnanecDto> zamestnanci)
        {
            // nepoužívam bulk z helpera, kedže bulk používa rodneCislo namiesto kod
            var errors = new List<object>();
            var success = new List<Zamestnanec>();
            var mapper = MapperConfig.InitializeAutomapper();

            foreach (var zamestnanecDTO in zamestnanci)
            {
                var zamestnanec = mapper.Map<Zamestnanec>(zamestnanecDTO);

                var (ok, result) = await CreateOrUpdateZamestnanecInternal(zamestnanec);

                if (ok && result is ObjectResult r1 && r1.Value is Zamestnanec zam)
                    success.Add(zam);
                else
                    errors.Add(new { rodneCislo = zamestnanec.RodneCislo, chyba = (result as ObjectResult)?.Value });
            }

            return Ok(new
            {
                uspesne = success.Count,
                neuspesne = errors.Count,
                chyby = errors
            });
        }

        private async Task<(bool success, ActionResult response)> CreateOrUpdateZamestnanecInternal(Zamestnanec zamestnanec)
        {
            var (success, response) = await CreateOrUpdate(
                entity: zamestnanec,
                keySelector: p => p.RodneCislo,
                keyValue: zamestnanec.RodneCislo,
                excludedProperties: new[] { "RodneCislo" },
                customValidation: async (p) =>
                {
                    //toto treba dat do funkcie alebo vymysliet ako validovat ak uz existuje nieco v tabulke
                    //lepsie, mozno do helper pridat dalsi funkciu it exists?, a naraz hodit vsetky ktore sa maju validovat
                   // takisto aj is valid, spravny format aky ma byt
                    if (string.IsNullOrWhiteSpace(zamestnanec.RodneCislo))
                    {
                        return (false, "Rodné číslo musí byť vyplnené." );
                    }
                    if (!string.IsNullOrWhiteSpace(p.RodneCislo))
                    {
                        var exists = await _context.Zamestnanci
                            .AnyAsync(z => z.RodneCislo == p.RodneCislo);
                        if (!exists)
                            return (false, "Rodné číslo neexistuje v tabuľke zamestnanci.");
                    }
                    var context = new ValidationContext(zamestnanec) { MemberName = nameof(Zamestnanec.RodneCislo) };
                    var results = new List<ValidationResult>();
                    bool isValidRC = Validator.TryValidateProperty(zamestnanec.RodneCislo, context, results);
                    bool isValidTel = Validator.TryValidateProperty(zamestnanec.TelefonneCislo, context, results);
                    if (!isValidRC)
                    {
                        var chyba = results.First().ErrorMessage;
                        return (false,  chyba);
                    }
                    if (isValidTel)
                    {
                        return (false, "Telefónne číslo je v zlom formáte (medzinárodný formát)");
                    }
                    if (!string.IsNullOrWhiteSpace(zamestnanec.TelefonneCislo))
                    {

                        var existujeTelefon = await _context.Zamestnanci
                             .AnyAsync(z => z.TelefonneCislo == zamestnanec.TelefonneCislo && z.RodneCislo != zamestnanec.RodneCislo);
                        if (existujeTelefon)
                        {
                            return (false, "Telefónne čislo je už zaregistrované." );
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(zamestnanec.Email) &&
                         await _context.Zamestnanci.AnyAsync(u => u.Email == zamestnanec.Email && u.RodneCislo != zamestnanec.RodneCislo))
                    {
                        return (false,"Tento email už je zaregistrovaný." );
                    }
                    return (true, null);
                },
                getActionName: nameof(GetZamestnanec),
                controllerName: "Projekt",
                routeValues: new { RodneCislo = zamestnanec.RodneCislo }
            );

            return (success, response);
        }

     

        //api/Zamestnanec/{rc}
        [HttpGet]
        public async Task<IActionResult> GetZamestnanec([FromQuery] string rc)
        {
            var zamestnanec = await _context.Zamestnanci
                .FirstOrDefaultAsync(z => z.RodneCislo == rc);

            if (zamestnanec == null)
            {
                return NotFound();
            }

            return Ok(zamestnanec);
        }

        [HttpGet]
        public async Task<ActionResult<List<ZamestnanecDto>>> GetAll()
        {
            return await GetAllEntities<Zamestnanec, ZamestnanecDto>(
                _context.Zamestnanci.Include(d => d.Oddelenia)
            );
        }

        [HttpDelete]
        public async Task<ActionResult<Zamestnanec>> DeleteZamestnanec([FromQuery] string rc)
        {
            var zamestnanec = await _context.Zamestnanci
                .FirstOrDefaultAsync(z => z.RodneCislo == rc);

            if (zamestnanec == null)
            {
                return NotFound();
            }

            _context.Zamestnanci.Remove(zamestnanec);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Zamestnanec bol úspešne odstránený." });
        }

    }
}

public class ZamestnanecDto
{
    public int Id { get; set; }

    public string? RodneCislo { get; set; }

    public string? Meno { get; set; }

    public string? Priezvisko { get; set; }

    public string? Email { get; set; }
    public string? Titul { get; set; }
    public string? TelefonneCislo { get; set; }
    public int? OddelenieId { get; set; }

    [JsonPropertyOrder(100)]
    public ICollection<Oddelenie>? Oddelenia { get; set; }

}