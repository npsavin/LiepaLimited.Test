using LiepaLimited.Test.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiepaLimited.Test.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<LiepaLimitedDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString(nameof(LiepaLimitedDbContext))));

            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            return services;
        }
    }
}
