using System;
using System.Threading;
using System.Threading.Tasks;
using LiepaLimited.Test.Application.Cache;
using LiepaLimited.Test.Domain;
using MediatR;

namespace LiepaLimited.Test.Application.Queries
{
    public class GetUserInfoQuery: IQuery<UserInfo>
    {
        public int Id { get; }

        public GetUserInfoQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfo>
    {
        private readonly ICacheService _cache;

        public GetUserInfoQueryHandler(ICacheService cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<UserInfo> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            return _cache.Get(request.Id);
        }
    }
}
