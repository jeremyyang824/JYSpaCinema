using AutoMapper;
using JYSpaCinema.Infrastructure.Mappings;

namespace JYSpaCinema.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                new DomainToViewModelMappingProfile().Register(cfg);
            });
        }
    }
}