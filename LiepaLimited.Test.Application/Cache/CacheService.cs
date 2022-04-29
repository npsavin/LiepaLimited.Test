using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiepaLimited.Test.Application.Exceptions;
using LiepaLimited.Test.Database;
using LiepaLimited.Test.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LiepaLimited.Test.Application.Cache
{
    public class CacheService : ICacheService
    {
        private Dictionary<int, UserInfo> _cache;
        private readonly IServiceScopeFactory _scopeFactory;
        public CacheService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory)); ;
        }

        public void Init()
        {
            using var scope = _scopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<LiepaLimitedDbContext>();
            _cache = context.Users.ToDictionary(x => x.Id, x => x);
        }

        public async Task UpdateCacheAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<LiepaLimitedDbContext>();
            var newDictionary = await context.Users.ToDictionaryAsync(x => x.Id, x => x, cancellationToken);
            _cache = newDictionary;
        }

        public UserInfo Get(int id)
        {
            if (!_cache.TryGetValue(id, out var user))
            {
                throw new UserNotFoundException($"User with id {id} not found");
            }
            return user;
        }
    }
}
