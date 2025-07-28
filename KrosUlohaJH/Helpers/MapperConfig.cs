using AutoMapper;
using KrosUlohaJH.Models;
using Microsoft.Extensions.Logging;

namespace KrosUlohaJH.Helpers
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
         
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Debug);
            });

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ZamestnanecDto, Zamestnanec>();
                cfg.CreateMap<DiviziaDto, Divizia>();
                cfg.CreateMap<Divizia, DiviziaDto>();
                cfg.CreateMap<FirmaDto, Firma>();
                cfg.CreateMap<Firma, FirmaDto>();
                cfg.CreateMap<OddeleniaDto, Oddelenie>();
                cfg.CreateMap<ProjektDto, Projekt>();
                cfg.CreateMap<Projekt, ProjektDto>();
            }, loggerFactory);
         
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
