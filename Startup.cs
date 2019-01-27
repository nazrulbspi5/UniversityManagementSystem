using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UoUWebApp.Startup))]
namespace UoUWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
