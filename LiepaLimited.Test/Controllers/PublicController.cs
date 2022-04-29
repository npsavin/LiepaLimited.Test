using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LiepaLimited.Test.Application.Dto;
using LiepaLimited.Test.Application.Queries;
using MediatR;

namespace LiepaLimited.Test.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class PublicController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PublicController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("UserInfo")]
        public async Task<IActionResult> GetUserInfoAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserInfoQuery(id), cancellationToken);
            return View(_mapper.Map<UserInfoDto>(result));
        }
    }
}
