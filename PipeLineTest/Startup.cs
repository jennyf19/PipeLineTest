using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PipeLineTest.Startup))]
namespace PipeLineTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
