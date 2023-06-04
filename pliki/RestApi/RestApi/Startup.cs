using DataAccessLibary;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASPNetCoreIdentity.Data;
using RestApi.options;

namespace RestApi
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
            services.AddDbContext<BdContext>( options => 
                options.UseSqlServer(Configuration.GetConnectionString("default"))
             );
           

            //services.AddSwaggerGen(x => x.SwaggerDoc("v1"));
         

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // var swagger_opt = new SwaggerOptions();

           // Configuration.GetSection(nameof(SwaggerOptions)).Bind(swagger_opt);

           // app.UseSwagger(options => { options.RouteTemplate = swagger_opt.JsonRoute; });
           
            //app.UseSwaggerUI(options => { options.SwaggerEndpoint(swagger_opt.UIEndpoint, swagger_opt.Description); });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {

                    var _context = context.RequestServices.GetRequiredService<BdContext>();
                    var model = _context.Order.Count();

                    await context.Response.WriteAsync(String.Join("",model));
                });
            });


        }
    }
}
