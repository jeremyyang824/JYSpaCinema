using System;

namespace JYSpaCinema.Service.Uow
{
    public interface IUnitOfWork
    {
        string Id { get; }

        void Begin(UnitOfWorkOptions options);

        void Commit();
    }
}
