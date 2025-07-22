using AutoMapper;
using KrosUlohaJH.Models;
using Microsoft.Extensions.Logging;

namespace KrosUlohaJH.Helpers
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Debug);
            });

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ZamestnanecDto, Zamestnanec>();
                cfg.CreateMap<DiviziaDto, Divizia>();
                cfg.CreateMap<FirmaDto, Firma>();
                cfg.CreateMap<OddeleniaDto, Oddelenie>();
                cfg.CreateMap<ProjektDto, Projekt>();
            }, loggerFactory);
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
