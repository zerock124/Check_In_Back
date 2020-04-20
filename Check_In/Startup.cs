using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Check_In.Startup))]
namespace Check_In
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
