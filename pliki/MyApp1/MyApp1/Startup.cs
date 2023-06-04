using DataAccessLibary;
using Microsoft.EntityFrameworkCore;
using MyApp1.options;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using MyApp1.Installers;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyApp1
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration conf)
        {
            Configuration = conf;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            var InstallerCollection = typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            services.InstallServices(Configuration,InstallerCollection);
        

        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var swaggerOpt = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOpt);

            app.UseSwagger(
                options => options.RouteTemplate = swaggerOpt.JsonRoute
                );
            app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerOpt.UIEndpoint, "v1"));



            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();

                endpoints.MapGet("/test", async context =>
                    {
                        var db = context.RequestServices.GetRequiredService<BdContext>();
                       
                        var c = db.Order.Select(x => x.AdresOdbiorcy).Count().ToString();
                        await context.Response.WriteAsync(c);
                    }
                ) ;
            });
        }
    }
}
