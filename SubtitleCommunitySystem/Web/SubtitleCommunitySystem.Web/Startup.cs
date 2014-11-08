using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SubtitleCommunitySystem.Web.Startup))]
namespace SubtitleCommunitySystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
