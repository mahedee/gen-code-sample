using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoleBasedAuth.Startup))]
namespace RoleBasedAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
