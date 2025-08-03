using AutoMapper;
using KrosUlohaJH.Helpers;
using KrosUlohaJH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
}