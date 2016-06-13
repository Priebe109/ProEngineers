using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Opgave2.Startup))]
namespace Opgave2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
