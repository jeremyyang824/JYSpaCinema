using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JYSpaCinema.Service;

namespace JYSpaCinema.Infrastructure.EntityFramework.Uow
{
    /// <summary>
    /// Uow的EntityFramework实现
    /// 主要职责：
    ///     1.DbContext的创建
    ///     2.DbContext的提交
    ///     3.事务控制（未实现）
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext dbContext;
        //TODO：IIocResolver

        public void Commit()
        {
            this.saveChanges();
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                this.dbContext.Dispose();
            }
        }

        private void saveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
