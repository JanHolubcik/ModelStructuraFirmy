using KrosUlohaJH.Models;
using Microsoft.AspNetCore.Mvc;

namespace KrosUlohaJH.Helpers
{
        public class BulkHelper
        {
            public static async Task<IActionResult> PostBulk<T, TEntity>(
                List<T> model,
                Func<TEntity, Task<(bool success, ActionResult response)>> CreateOrUpdate,
                string identifierPropertyName = "Kod")
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
                        // Get the identifier value using reflection
                        var identifierValue = GetPropertyValue(dto, identifierPropertyName);
                        errors.Add(new
                        {
                            identifier = identifierValue,
                            chyba = (result as ObjectResult)?.Value
                        });
                    }
                }

                return new OkObjectResult(new
                {
                    uspesne = success.Count,
                    neuspesne = errors.Count,
                    chyby = errors
                });
            }

            private static object GetPropertyValue(object obj, string propertyName)
            {
                var property = obj.GetType().GetProperty(propertyName);
                return property?.GetValue(obj);
            }
        }


    }
