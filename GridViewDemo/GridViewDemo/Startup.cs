using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GridViewDemo.Startup))]
namespace GridViewDemo
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
