using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SendGrid.ASPSamples.Startup))]
namespace SendGrid.ASPSamples
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
