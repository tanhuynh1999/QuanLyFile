using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuanLyFile.Startup))]
namespace QuanLyFile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
