using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VS13CR11.Startup))]
namespace VS13CR11
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
