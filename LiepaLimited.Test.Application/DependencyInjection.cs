using System.Reflection;
using LiepaLimited.Test.Application.Cache;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LiepaLimited.Test.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}
