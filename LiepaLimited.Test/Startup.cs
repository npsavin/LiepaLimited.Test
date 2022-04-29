using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LiepaLimited.Test.Application;
using LiepaLimited.Test.Application.Cache;
using LiepaLimited.Test.Database;
using LiepaLimited.Test.Handlers;
using LiepaLimited.Test.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

namespace LiepaLimited.Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddControllers()
                .AddNewtonsoftJson()
                .AddXmlSerializerFormatters();

            services.AddDatabase(Configuration);
            services.AddApplication();

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                    ("BasicAuthentication", null);
            services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LiepaLimited.Test", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ICacheService cache)
        {
            
            cache.Init();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LiepaLimited.Test v1"));

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
