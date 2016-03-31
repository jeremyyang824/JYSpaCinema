using System;

namespace JYSpaCinema.Service.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        string Id { get; }

        void Begin(UnitOfWorkOptions options);

        void SaveChanges();

        void Commit();
    }
}
