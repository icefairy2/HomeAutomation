using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Owin;
using Owin;
using webapp.Controllers;

[assembly: OwinStartup(typeof(webapp.Startup))]

namespace webapp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("Data Source=DESKTOP-NLQQARQ;Initial Catalog=hangfire;Integrated Security=True");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(() => TemperatureUpdater.UpdateTemperatureToDb(), Cron.Minutely);

        }
    }
}
