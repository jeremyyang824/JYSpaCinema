using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;
using JYSpaCinema.Service.DTO;
using JYSpaCinema.Service.Uow;
using JYSpaCinema.Domain;

namespace JYSpaCinema.Service.AppServices
{
    public class CustomerAppService : BaseAppService
    {
        private readonly ICustomerRepository _customersRepository;

        public CustomerAppService(
            ICustomerRepository customersRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(unitOfWorkManager)
        {
            this._customersRepository = customersRepository;
        }

        /// <summary>
        /// 根据客户ID获取客户信息
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <returns>客户信息</returns>
        public CustomerDto GetCustomer(int id)
        {
            var customer = this._customersRepository.GetByKey(id);
            return customer.MapTo<CustomerDto>();
        }

        /// <summary>
        /// 根据过滤条件获取客户信息
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns>客户信息</returns>
        public IEnumerable<CustomerDto> GetCustomers(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                throw new DomainException("Customer filters can not be none!");

            filter = filter.ToLower().Trim();
            var customers = this._customersRepository.GetAll()
                .Where(c => c.Email.ToLower().Contains(filter)
                    || c.FirstName.ToLower().Contains(filter)
                    || c.LastName.ToLower().Contains(filter)).ToList();

            return customers.MapTo<IEnumerable<CustomerDto>>();
        }

        /// <summary>
        /// 注册新客户并返回
        /// </summary>
        /// <param name="customer">新客户</param>
        /// <returns>新客户</returns>
        public CustomerDto Register(CustomerDto customer)
        {
            using (IUnitOfWork uow = this.UnitOfWorkManager.Begin(UnitOfWorkOptions.Default))
            {
                if (this._customersRepository.UserExists(customer.Email, customer.IdentityCard))
                    throw new DomainException("Email or Identity Card number already exists");

                Customer newCustomer = new Customer();
                this.updateCustomer(newCustomer, customer);
                this._customersRepository.Add(newCustomer);

                uow.Commit();

                return newCustomer.MapTo<CustomerDto>();
            }
        }

        /// <summary>
        /// 更新指定客户
        /// </summary>
        /// <param name="customer">更新客户及信息</param>
        public void Update(CustomerDto customer)
        {
            if (customer.ID <= 0)
                throw new ArgumentException("customer");

            using (IUnitOfWork uow = this.UnitOfWorkManager.Begin(UnitOfWorkOptions.Default))
            {
                Customer existCustomer = this._customersRepository.GetByKey(customer.ID);
                if (existCustomer == null)
                    throw new DomainException("Customer [{0}] not exists!", customer.ID);

                this.updateCustomer(existCustomer, customer);
                this._customersRepository.Update(existCustomer);

                uow.Commit();
            }
        }

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>客户信息分页集合</returns>
        public PaginationResult<CustomerDto> Search(int page = 1, int pageSize = 15, string filter = null)
        {
            Expression<Func<Customer, bool>> searchExpression = null;
            if (filter != null)
            {
                filter = filter.ToLower().Trim();
                searchExpression = (c =>
                    c.LastName.ToLower().Contains(filter)
                    || c.FirstName.ToLower().Contains(filter)
                    || c.IdentityCard.ToLower().Contains(filter));
            }
            //IPagedList<Customer> result = this._customersRepository.FindByPager(searchExpression, page, pageSize,
            //    new SortExpression<Customer>[]
            //    {
            //        new SortExpression<Customer>(c=>c.LastName,SortOrder.Ascending),
            //        new SortExpression<Customer>(c=>c.FirstName,SortOrder.Ascending),
            //    });

            //return result.MapToPagination<CustomerDto>();

            var query = this._customersRepository.GetAll().WhereIf(searchExpression);
            var totalCount = query.Count();
            var items = query
                .OrderBy(c => c.LastName).ThenBy(c => c.FirstName)
                .PageBy((page - 1) * pageSize, pageSize)
                .ToList()
                .MapTo<IReadOnlyList<CustomerDto>>();
            return new PaginationResult<CustomerDto>(totalCount, items);
        }

        private void updateCustomer(Customer customer, CustomerDto source)
        {
            customer.FirstName = source.FirstName;
            customer.LastName = source.LastName;
            customer.IdentityCard = source.IdentityCard;
            customer.Mobile = source.Mobile;
            customer.DateOfBirth = source.DateOfBirth;
            customer.Email = source.Email;
            customer.RegistrationDate = (customer.RegistrationDate == DateTime.MinValue ? DateTime.Now : source.RegistrationDate);
        }
    }
}
