using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projekat.Startup))]
namespace Projekat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
