using JYSpaCinema.Service.Uow;

namespace JYSpaCinema.Service.AppServices
{
    public abstract class BaseAppService
    {
        public IUnitOfWorkManager UnitOfWorkManager { get; }

        protected BaseAppService(IUnitOfWorkManager unitOfWorkManager)
        {
            this.UnitOfWorkManager = unitOfWorkManager;
        }
    }
}
