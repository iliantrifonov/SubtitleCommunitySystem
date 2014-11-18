namespace SubtitleCommunitySystem.Web
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Reflection;
    using System.Threading;
    using System.Globalization;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Data.Migrations;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void ConfigureAutoMapper()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(Assembly.GetExecutingAssembly());
        }

        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            EngineConfig.RegisterEngines(ViewEngines.Engines);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(Assembly.GetExecutingAssembly());

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }        
    }
}
