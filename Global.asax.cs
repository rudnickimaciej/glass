using Autofac;
using Autofac.Integration.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Translate.Controllers;
using Translate.Models.Services;
using Translate.ViewModels.Builders;

namespace Translate
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfigTest.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);

            builder.Register(c => new ForumService()).As<IForum>();
            builder.Register(c => new ApplicationUserService()).As<IApplicationUser>();
            builder.Register(c => new VoteService()).As<IVoteService>();
            builder.Register(c => new UploadService()).As<IUpload>();
            builder.Register(c => new ViewModelBuilder()).As<IViewModelBuilder>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }


    }
}

