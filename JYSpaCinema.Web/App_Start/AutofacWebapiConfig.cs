using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using JYSpaCinema.Domain.Repositories;
using JYSpaCinema.Domain.Services;
using JYSpaCinema.Infrastructure.EntityFramework;
using JYSpaCinema.Infrastructure.EntityFramework.Repositories;
using JYSpaCinema.Infrastructure.EntityFramework.Uow;
using JYSpaCinema.Infrastructure.Services;
using JYSpaCinema.Service.AppServices;
using JYSpaCinema.Service.Uow;

namespace JYSpaCinema.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container { get; private set; }

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterSerivces(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            //设置WebAPI Resolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterSerivces(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //EF
            builder.RegisterType<JYSpaCinemaDbContext>()
                .As<DbContext>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(DefaultDbContextProvider<>))
                .As(typeof(IDbContextProvider<>))
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(EFBaseRepository<,>))
                .As(typeof(IRepository<,>))
                .InstancePerRequest();

            builder.RegisterType(typeof(EFUnitOfWork<DbContext>))
                .As(typeof(IUnitOfWork))
                .InstancePerRequest();

            //Domain Service
            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>()
                .InstancePerRequest();

            builder.RegisterType<MembershipService>()
                .As<IMembershipService>()
                .InstancePerRequest();

            //Domain Repository
            builder.RegisterType<CustomerRepository>()
                .As<ICustomerRepository>()
                .InstancePerRequest();

            builder.RegisterType<StockRepository>()
                .As<IStockRepository>()
                .InstancePerRequest();

            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerRequest();

            builder.RegisterType<RoleRepository>()
               .As<IRoleRepository>()
               .InstancePerRequest();

            builder.RegisterType<GenreRepository>()
                .As<IGenreRepository>()
                .InstancePerRequest();

            builder.RegisterType<MovieRepository>()
                .As<IMovieRepository>()
                .InstancePerRequest();

            //Domain Infrastructure
            builder.RegisterType<UnitOfWorkManager>()
                .As<IUnitOfWorkManager>()
                .InstancePerRequest();

            //AppService
            builder.RegisterType<AccountAppService>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<CustomerAppService>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<GenreAppService>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<MovieAppService>()
               .AsSelf()
               .InstancePerRequest();

            Container = builder.Build();
            return Container;
        }
    }
}