using ComicsShopOnline.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Optimization;
using ComicsShopOnline.BusinessLogic.Data;
using System.Data.Entity;
using AutoMapper;
using ComicsShopOnline.Web.Profiles;

namespace ComicsShopOnline.Web
{
    public class Global : HttpApplication
    {
        public static IMapper MapperInstance;
        void Application_Start(object sender, EventArgs e)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<ComicProfile>();
                cfg.AddProfile<CartProfile>();
            });

            MapperInstance = config.CreateMapper();

            AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalFilters.Filters.Add(new ComicsShopOnline.Web.Filters.AdminModeAttribute());

            BundleConfig.RegisterBundles(BundleTable.Bundles);

           
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
        }

    }
}