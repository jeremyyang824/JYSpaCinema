using JYSpaCinema.Domain.Entities;

namespace JYSpaCinema.Infrastructure.EntityFramework.Configurations
{
    public class GenreConfiguration : EntityBaseConfiguration<Genre, int>
    {
        public GenreConfiguration()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(50);
        }
    }
}
