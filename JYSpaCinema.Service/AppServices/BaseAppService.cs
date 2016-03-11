using JYSpaCinema.Service.Uow;

namespace JYSpaCinema.Service.AppServices
{
    public abstract class BaseAppService
    {
        private readonly IUnitOfWork _unitOfWork;

        protected BaseAppService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
    }
}
