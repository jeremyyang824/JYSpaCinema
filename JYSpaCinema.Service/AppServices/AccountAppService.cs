using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Services;
using JYSpaCinema.Domain.ValueObjects;
using JYSpaCinema.Service.Uow;
using JYSpaCinema.Service.DTO;

namespace JYSpaCinema.Service.AppServices
{
    public class AccountAppService : BaseAppService
    {
        private readonly IMembershipService _membershipService;

        public AccountAppService(IMembershipService membershipService, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._membershipService = membershipService;
        }

        public bool Login(LoginInputDto loginDto)
        {
            MembershipContext userContext = this._membershipService.ValidateUser(loginDto.Username, loginDto.Password);
            return userContext.User != null;
        }

        public bool Register(RegistrationInputDto registrationDto)
        {
            //this._unitOfWork.Begin(UnitOfWorkOptions.Default);

            User user = this._membershipService.CreateUser(registrationDto.Username, registrationDto.Email, registrationDto.Password);
            this._unitOfWork.Commit();

            this._membershipService.AddRoles(user, new int[] { 1 });
            this._unitOfWork.Commit();

            return user != null;
        }
    }
}
