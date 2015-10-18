using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LiveCC2015.Data;
using System.Web.Mvc;
using LiveCC2015.Web.Workers;

namespace LiveCC2015.Web
{
    public static class AutofacConfig
    {
        public static void RegisterContainer()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            var currentAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterApiControllers(currentAssembly);
            builder.RegisterControllers(currentAssembly);

            builder.RegisterType<EventRepository>().InstancePerRequest();
            builder.RegisterType<EventsHubContext>().InstancePerRequest();

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
