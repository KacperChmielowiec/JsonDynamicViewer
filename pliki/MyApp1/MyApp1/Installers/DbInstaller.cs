using DataAccessLibary;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace MyApp1.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<BdContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("default")


                );

            });
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("users")


                );

            });

            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedAccount = false;
                   
                    
                }
                ).AddEntityFrameworkStores<IdentityContext>();

            
        }
    }
}
