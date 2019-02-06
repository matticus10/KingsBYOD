using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kings_BYOD_Helpdesk.Startup))]
namespace Kings_BYOD_Helpdesk
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
