using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Service.DTO;

namespace JYSpaCinema.Infrastructure.Mappings
{
    public class DomainToViewModelMappingProfile : IMapperRegistrar
    {
        public void Register(IProfileExpression mapperConfig)
        {
            mapperConfig.CreateMap<Customer, CustomerDto>();
        }
    }
}
