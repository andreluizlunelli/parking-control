using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(parking_control.Startup))]
namespace parking_control
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
