using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Issues.Startup))]
namespace Issues
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
