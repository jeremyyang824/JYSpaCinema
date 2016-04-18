using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Transactions;
using JYSpaCinema.Service.Uow;

namespace JYSpaCinema.Infrastructure.EntityFramework.Uow
{
    /// <summary>
    /// Uow的EntityFramework实现
    /// 主要职责：
    ///     1.DbContext的创建
    ///     2.DbContext的提交
    ///     3.事务控制（未实现）
    /// </summary>
    public class EFUnitOfWork<TDbContext> : IUnitOfWork, IDisposable
         where TDbContext : DbContext
    {
        private bool _isBeginCalledBefore;
        private bool _isCompleteCalledBefore;
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        protected TransactionScope CurrentTransaction;
        protected DbContext DbContext
        {
            get { return this._dbContextProvider.DbContext; }
        }

        public string Id { get; private set; }
        public bool IsDisposed { get; private set; }


        public EFUnitOfWork(IDbContextProvider<TDbContext> dbContextProvider)
        {
            this.Id = Guid.NewGuid().ToString("N");
            this._dbContextProvider = dbContextProvider;
        }

        public void Begin(UnitOfWorkOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            if (this._isBeginCalledBefore)
                throw new InvalidOperationException("This unit of work has started before.");
            this._isBeginCalledBefore = true;

            //Core Process
            if (options.IsTransactional)
            {
                this.CurrentTransaction = new TransactionScope(options.Scope, new TransactionOptions
                {
                    IsolationLevel = options.IsolationLevel,
                    Timeout = options.Timeout
                });
            }
        }

        public void SaveChanges()
        {
            if (this.DbContext != null)
                this.DbContext.SaveChanges();
        }

        public void Commit()
        {
            try
            {
                if (this._isCompleteCalledBefore)
                    throw new InvalidOperationException("Complete is called before!");
                this._isCompleteCalledBefore = true;

                //Core Process
                if (this.DbContext != null)
                    this.DbContext.SaveChanges();

                if (this.CurrentTransaction != null)
                    this.CurrentTransaction.Complete();

                //TODO：OnSuccess
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Dispose()
        {
            if (this.IsDisposed)
                return;

            this.IsDisposed = true;

            if (this.DbContext != null)
                this.DbContext.Dispose();

            if (this.CurrentTransaction != null)
                this.CurrentTransaction.Dispose();
        }

    }
}
