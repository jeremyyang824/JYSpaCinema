using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using JYSpaCinema.Domain.Repositories;
using JYSpaCinema.Infrastructure.EntityFramework;
using JYSpaCinema.Infrastructure.EntityFramework.Repositories;
using JYSpaCinema.Infrastructure.EntityFramework.Uow;
using JYSpaCinema.Service.Uow;

namespace JYSpaCinema.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container { get; private set; }

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

            builder.RegisterGeneric(typeof(EFBaseRepository<,,>))
                .As(typeof(IRepository<,>))
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(EFUnitOfWork<>))
                .As(typeof(IUnitOfWork))
                .InstancePerRequest();

            return Container;
        }
    }
}