using System;

namespace JYSpaCinema.Service.Uow
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork Begin(UnitOfWorkOptions options);
    }
}
