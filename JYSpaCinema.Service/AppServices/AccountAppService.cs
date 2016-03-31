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

        public AccountAppService(
            IMembershipService membershipService, 
            IUnitOfWorkManager unitOfWorkManager)
            : base(unitOfWorkManager)
        {
            this._membershipService = membershipService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        public MembershipContext Login(LoginInputDto loginDto)
        {
            MembershipContext userContext = this._membershipService.ValidateUser(loginDto.Username, loginDto.Password);
            return userContext;
        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        public bool Register(RegistrationInputDto registrationDto)
        {
            using (IUnitOfWork uow = this.UnitOfWorkManager.Begin(UnitOfWorkOptions.Default))
            {
                User user = this._membershipService.CreateUser(registrationDto.Username, registrationDto.Email, registrationDto.Password);
                uow.SaveChanges();

                this._membershipService.AddRoles(user, new int[] { 1 });
                uow.Commit();

                return user != null;
            }
        }


    }
}
