using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LiepaLimited.Test
{
    public static class HostExtensions
    {
        public static IHost MigrateToLatestVersion<TDbContext>(this IHost host) where TDbContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<TDbContext>();
            context.Database.Migrate();
            return host;
        }
    }
}
