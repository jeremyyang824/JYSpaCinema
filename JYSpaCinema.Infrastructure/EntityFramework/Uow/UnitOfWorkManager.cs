using System;
using System.Collections.Generic;
using Autofac;
using JYSpaCinema.Service.Uow;

namespace JYSpaCinema.Infrastructure.EntityFramework.Uow
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly IComponentContext _context;

        public UnitOfWorkManager(IComponentContext context)
        {
            this._context = context;
        }

        public IUnitOfWork Begin(UnitOfWorkOptions options)
        {
            IUnitOfWork uow = this._context.Resolve<IUnitOfWork>();
            uow.Begin(options);
            return uow;
        }
    }
}
