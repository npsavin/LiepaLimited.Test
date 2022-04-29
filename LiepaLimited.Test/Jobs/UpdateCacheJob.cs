using System;
using System.Threading.Tasks;
using LiepaLimited.Test.Application.Command;
using MediatR;
using Quartz;

namespace LiepaLimited.Test.Jobs
{
    [DisallowConcurrentExecution]
    public class UpdateCacheJob : IJob
    {
        private readonly IMediator _mediator;

        public UpdateCacheJob(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new UpdateCacheCommand());
        }


    }
}
