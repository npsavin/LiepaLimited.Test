using System;
using System.Threading;
using System.Threading.Tasks;
using LiepaLimited.Test.Domain;
using Microsoft.EntityFrameworkCore;

namespace LiepaLimited.Test.Database.Repositories
{
    public class UserInfoRepository: IUserInfoRepository
    {
        private readonly LiepaLimitedDbContext _context;
        public UserInfoRepository(LiepaLimitedDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserInfo> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> AnyAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<UserInfo> AddAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<UserInfo> RemoveAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<UserInfo> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }


    }
}
