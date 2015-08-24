using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookYourFood.Startup))]
namespace BookYourFood
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DataProtectionProvider = ApplicationUserBC.Configuration.Startup.ConfigureAuth(app);
        }
    }
}
