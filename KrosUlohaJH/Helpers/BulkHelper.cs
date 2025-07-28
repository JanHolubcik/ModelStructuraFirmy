using KrosUlohaJH.Models;
using Microsoft.AspNetCore.Mvc;

namespace KrosUlohaJH.Helpers
{
    public class BulkHelper
    {

        public async Task<IActionResult> PostBulk<T, TEntity>(List<T> model, Func<TEntity, Task<(bool, IActionResult)>> CreateOrUpdate)
            where T : BaseModel //  T definujem ako BaseModel
            where TEntity : BaseModel 
        {
            var errors = new List<object>();
            var success = new List<T>();
            var mapper = MapperConfig.InitializeAutomapper();

            foreach (var dto in model)
            {
                var entity = mapper.Map<TEntity>(dto);
                var (isSuccess, result) = await CreateOrUpdate(entity);

                if (isSuccess && result is OkObjectResult okResult && okResult.Value is TEntity responseEntity)
                {
                    var mappedBack = mapper.Map<T>(responseEntity);
                    success.Add(mappedBack);
                }
                else
                {
                    errors.Add(new { kod = dto.Kod, chyba = (result as ObjectResult)?.Value });
                }
            }

            return new OkObjectResult(new
            {
                uspesne = success.Count,
                neuspesne = errors.Count,
                chyby = errors
            });
        }
    }
}
