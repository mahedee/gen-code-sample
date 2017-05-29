using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MasterDetailsMVC.Startup))]
namespace MasterDetailsMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
