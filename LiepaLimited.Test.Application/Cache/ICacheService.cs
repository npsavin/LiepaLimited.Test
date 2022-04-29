using System.Threading;
using System.Threading.Tasks;
using LiepaLimited.Test.Domain;

namespace LiepaLimited.Test.Application.Cache
{
    public interface ICacheService
    {
        void Init();
        Task UpdateCacheAsync( CancellationToken cancellationToken);
        UserInfo Get(int id);
    }
}
