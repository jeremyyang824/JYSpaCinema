using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace JYSpaCinema.Infrastructure.EntityFramework
{
    public class DefaultDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>, IDisposable
        where TDbContext : DbContext
    {
        private bool _isDisposed;

        public TDbContext DbContext { get; private set; }

        public DefaultDbContextProvider(TDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void Dispose()
        {
            this.dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                if (this.DbContext != null)
                    this.DbContext.Dispose();
            }
            _isDisposed = true;
        }

        ~DefaultDbContextProvider()
        {
            this.dispose(false);
        }
    }
}
