﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;
using JYSpaCinema.Domain.Services;
using JYSpaCinema.Domain.ValueObjects;
using JYSpaCinema.Service.Uow;
using JYSpaCinema.Infrastructure.EntityFramework.Repositories;

namespace JYSpaCinema.Infrastructure.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IEncryptionService encryptionService)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._encryptionService = encryptionService;
        }

        public User CreateUser(string username, string email, string password)
        {
            var existingUser = this._userRepository.GetSingleByUsername(username);
            if (existingUser != null)
                throw new JYSpaCinemaDomainException("Username [{0}] is already in user!", username);

            var passwordSalt = this._encryptionService.CreateSalt();
            var user = new User
            {
                UserName = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = this._encryptionService.EncryptPassword(password, passwordSalt),
                DateCreated = DateTime.Now
            };
            this._userRepository.Add(user);
            return user;
        }

        public void AddRoles(User user, int[] roles)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (roles != null && roles.Length > 0)
            {
                foreach (var roleId in roles)
                {
                    this.addUserToRole(user, roleId);
                }
            }
            this._userRepository.Update(user);
        }

        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipContext = new MembershipContext(username);

            var user = this._userRepository.GetSingleByUsername(username);
            if (user != null && isUserValid(user, password))
            {
                membershipContext.IsAuthenticated = true;
                membershipContext.UserId = user.ID;
                membershipContext.Email = user.Email;
                membershipContext.Roles = user.Roles.Select(r => r.Name.ToString()).Distinct().ToArray();
            }
            return membershipContext;
        }

        private void addUserToRole(User user, int roleId)
        {
            var role = this._roleRepository.GetByKey(roleId);
            if (role == null)
                throw new JYSpaCinemaDomainException("Role doesn't exists.");

            if (user.Roles.All(r => r.ID != roleId))
            {
                user.Roles.Add(role);
            }
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(this._encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool isUserValid(User user, string password)
        {
            if (this.isPasswordValid(user, password))
                return !user.IsLocked;
            return false;
        }

    }
}
