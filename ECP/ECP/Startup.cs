using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECP.Startup))]
namespace ECP
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
