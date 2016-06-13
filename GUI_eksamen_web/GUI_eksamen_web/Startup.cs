using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GUI_eksamen_web.Startup))]
namespace GUI_eksamen_web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
