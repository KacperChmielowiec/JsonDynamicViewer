namespace MyApp1.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSwaggerGen(
                options => options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo())
                );
            services.AddMvc();
        }

    }
    
}
