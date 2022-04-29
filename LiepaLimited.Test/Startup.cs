using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LiepaLimited.Test.Application;
using LiepaLimited.Test.Application.Cache;
using LiepaLimited.Test.Database;
using LiepaLimited.Test.Handlers;
using LiepaLimited.Test.Jobs;
using LiepaLimited.Test.Middleware;
using Microsoft.AspNetCore.Authentication;
using Quartz;

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

            ConfigureJob(services);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ICacheService cache)
        {
            
            cache.Init();
            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureJob(IServiceCollection services)
        {
            const string jobIdentity = "UpdateCacheJob";
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseDefaultThreadPool(1);
                var jobKey = new JobKey(jobIdentity);
                q.AddJob<UpdateCacheJob>(opt => opt.WithIdentity(jobKey));
                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity(jobIdentity)
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(10).RepeatForever()));
            });
            services.AddQuartzServer(
                q =>
                {
                    q.AwaitApplicationStarted = true;
                    q.WaitForJobsToComplete = true;
                });

        }
    }
}
