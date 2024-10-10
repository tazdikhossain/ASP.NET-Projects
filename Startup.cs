using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClothesManagementSystem.Startup))]
namespace ClothesManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
