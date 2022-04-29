using System.Threading;
using System.Threading.Tasks;
using LiepaLimited.Test.Domain;

namespace LiepaLimited.Test.Database.Repositories
{
    public interface IUserInfoRepository
    {
        Task<UserInfo> GetAsync(int id);
        Task<bool> AnyAsync(int id, CancellationToken cancellationToken);
        Task<UserInfo> AddAsync(UserInfo user, CancellationToken cancellationToken);
        Task<UserInfo> RemoveAsync(int id, CancellationToken cancellationToken);
        Task<UserInfo> UpdateAsync(UserInfo user, CancellationToken cancellationToken);
    }
}
