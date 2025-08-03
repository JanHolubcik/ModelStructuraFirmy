using AutoMapper;
using KrosUlohaJH.Helpers;
using KrosUlohaJH.Models;
using Microsoft.AspNetCore.Mvc;

public abstract class BaseApiController : ControllerBase
{
    protected readonly StrukturaFirmyContext _context;
    protected readonly IMapper _mapper;

    protected BaseApiController(StrukturaFirmyContext context)
    {
        _context = context;
        _mapper = MapperConfig.InitializeAutomapper();
    }
}