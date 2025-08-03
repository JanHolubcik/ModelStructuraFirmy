using AutoMapper;
using KrosUlohaJH.Helpers;
using KrosUlohaJH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public abstract class BaseApiController : ControllerBase
{
    protected readonly StrukturaFirmyContext _context;
    protected readonly IMapper _mapper;

    protected BaseApiController(StrukturaFirmyContext context)
    {
        _context = context;
        _mapper = MapperConfig.InitializeAutomapper();
    }

    protected async Task<ActionResult<List<TDto>>> GetAllEntities<TEntity, TDto>(
        IQueryable<TEntity> query
    )
    where TEntity : class
    {
        var entities = await query.ToListAsync();
        var mapped = _mapper.Map<List<TDto>>(entities);
        return Ok(mapped);
    }

    protected async Task<(bool success, ActionResult response)> CreateOrUpdate<TEntity, TKey>(
        TEntity entity,
        Expression<Func<TEntity, TKey>> keySelector,
        TKey keyValue,
        string[] excludedProperties = null,
        Func<TEntity, Task<(bool isValid, string errorMessage)>> customValidation = null,
        string getActionName = null,
        string controllerName = null,
        object routeValues = null)
        where TEntity : class
    {
     
        if (customValidation != null)
        {
            var (isValid, errorMessage) = await customValidation(entity);
            if (!isValid)
                return (false, BadRequest(errorMessage));
        }
 
        var existingEntity = await _context.Set<TEntity>()
            .FirstOrDefaultAsync(BuildEqualityExpression(keySelector, keyValue));

        if (existingEntity != null)
        {

            var defaultExcludedProperties = new[] { "Id" };
            var propertiesToExclude = excludedProperties != null
                ? excludedProperties.Concat(defaultExcludedProperties).ToArray()
                : defaultExcludedProperties;

            ReplaceValuesOfObject.UpdateNonNullProperties<TEntity>(existingEntity, entity, propertiesToExclude);
            await _context.SaveChangesAsync();
            return (true, new OkObjectResult(existingEntity));
        }

        var (modelIsValid, modelState) = ValidationHelper.ValidateAndHandleModelState(entity, ModelState);
        if (!modelIsValid)
        {
            return (modelIsValid, new BadRequestObjectResult(modelState));
        }

 
        if (await _context.Set<TEntity>().AnyAsync(BuildEqualityExpression(keySelector, keyValue)))
        {
            return (false, new ConflictObjectResult(new { sprava = $"{typeof(TEntity).Name} už existuje" }));
        }


        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();


        if (!string.IsNullOrEmpty(getActionName))
        {
            return (true, new CreatedAtActionResult(getActionName, controllerName, routeValues, entity));
        }

        return (true, new CreatedResult("", entity));
    }


    private Expression<Func<TEntity, bool>> BuildEqualityExpression<TEntity, TKey>(
        Expression<Func<TEntity, TKey>> keySelector,
        TKey keyValue)
    {
        var parameter = keySelector.Parameters[0];
        var body = Expression.Equal(keySelector.Body, Expression.Constant(keyValue));
        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }
}