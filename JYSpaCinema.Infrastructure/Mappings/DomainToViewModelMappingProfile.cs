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
            mapperConfig.CreateMap<Genre, GenreDto>();
            mapperConfig.CreateMap<Movie, MovieDto>()
                .ForMember(vm => vm.Genre, map => map.MapFrom(m => m.Genre.Name))
                .ForMember(vm => vm.GenreId, map => map.MapFrom(m => m.Genre.ID))
                .ForMember(vm => vm.IsAvailable, map => map.MapFrom(m => m.Stocks.Any(s => s.IsAvailable)))
                .ForMember(vm => vm.NumberOfStocks, map => map.MapFrom(m => m.Stocks.Count))
                .ForMember(vm => vm.Image, map => map.MapFrom(m => string.IsNullOrEmpty(m.Image) == true ? "unknown.jpg" : m.Image));
        }
    }
}
