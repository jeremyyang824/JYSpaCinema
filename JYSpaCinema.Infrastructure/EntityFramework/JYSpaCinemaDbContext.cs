using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JYSpaCinema.Infrastructure.EntityFramework
{
    public class JYSpaCinemaDbContext : DbContext
    {
        public JYSpaCinemaDbContext()
            : base("JYSpaCinema")
        {
            Database.SetInitializer<JYSpaCinemaDbContext>(null);
        }

        #region Entity Sets

        #endregion

    }
}
