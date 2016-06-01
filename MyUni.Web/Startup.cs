using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gurukul.Web.Startup))]
namespace Gurukul.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
