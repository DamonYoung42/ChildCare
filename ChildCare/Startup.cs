using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChildCare.Startup))]
namespace ChildCare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
