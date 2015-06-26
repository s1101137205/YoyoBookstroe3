using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YoYoBookStore.Startup))]
namespace YoYoBookStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
