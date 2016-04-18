using AutoMapper;

namespace JYSpaCinema.Infrastructure.Mappings
{
    /// <summary>
    /// AutoMapper Self Register
    /// </summary>
    public interface IMapperRegistrar
    {
        void Register(IProfileExpression mapperConfig);
    }
}
