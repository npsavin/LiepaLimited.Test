using System;
using System.Threading;
using System.Threading.Tasks;
using LiepaLimited.Test.Application.Cache;
using MediatR;

namespace LiepaLimited.Test.Application.Command
{
    public class UpdateCacheCommand: ICommand
    {

    }

    internal class UpdateCacheCommandHandler : IRequestHandler<UpdateCacheCommand>
    {
        private readonly ICacheService _cache;

        public UpdateCacheCommandHandler(ICacheService cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<Unit> Handle(UpdateCacheCommand request, CancellationToken cancellationToken)
        {
            await _cache.UpdateCacheAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
