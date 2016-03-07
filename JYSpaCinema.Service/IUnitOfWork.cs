using System;

namespace JYSpaCinema.Service
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
