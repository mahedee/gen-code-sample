using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web.HRM.Startup))]
namespace Web.HRM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
