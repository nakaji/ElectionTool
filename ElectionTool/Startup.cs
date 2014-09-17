using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElectionTool.Startup))]
namespace ElectionTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
