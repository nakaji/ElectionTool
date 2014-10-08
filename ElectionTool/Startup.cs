using System.Data.Entity;
using ElectionTool.Migrations;
using ElectionTool.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElectionTool.Startup))]
namespace ElectionTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            ConfigureAuth(app);
        }
    }
}
