using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AdivinaQuienSoyService.Startup))]

namespace AdivinaQuienSoyService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}