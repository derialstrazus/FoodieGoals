using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(FoodieGoals.Startup))]

namespace FoodieGoals
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            ConfigureAuth(app);

            var corsPolicy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };

            //var origins = ConfigurationManager.AppSettings[Constants.CorsOriginsSettingKey];
            //if (origins != null) {
            //    foreach (var origin in origins.Split(';'))
            //    {
            //        corsPolicy.Origins.Add(origin);
            //    }
            //} else {
            //    corsPolicy.AllowAnyOrigin = true;
            //}

            //TODO: move these to config.  See commented code above.
            corsPolicy.Origins.Add("http://web.foodiegoals.local");
            corsPolicy.Origins.Add("http://foodiegoals.azurewebsites.net");

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(corsPolicy)
                }
            };

            app.UseCors(corsOptions);
        }
    }
}
