using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(xmlDemo.Startup))]
namespace xmlDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
