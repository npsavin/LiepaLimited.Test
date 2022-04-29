using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LiepaLimited.Test.Application.Command;
using LiepaLimited.Test.Application.Dto;
using LiepaLimited.Test.Application.Exceptions;
using LiepaLimited.Test.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LiepaLimited.Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("CreateUser")]
        [Produces("application/xml")]
        [Consumes("application/xml")]
        [ProducesResponseType(typeof(UserInfoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.Forbidden)]
        public async Task<CreateUserResponseDto> CreateUserAsync([FromBody] CreateUserRequestDto createReequestDto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateUserCommand(_mapper.Map<UserInfo>(createReequestDto.User)), cancellationToken);
            return new CreateUserResponseDto(_mapper.Map<UserInfoDto>(result));
        }

        [HttpPost("RemoveUser")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.Forbidden)]
        public async Task<RemoveUserResponseDto> RemoveUserAsync(RemoveUserRequestDto removeRequestDto, CancellationToken cancellationToken)
        {
            if (removeRequestDto?.RemoveUser == null)
                throw new BadRequestException("Request can't be null");

            var result = await _mediator.Send(new RemoveUserCommand(removeRequestDto.RemoveUser.Id), cancellationToken);
            return new RemoveUserResponseDto(_mapper.Map<UserInfoDto>(result));

        }

        [HttpPost("SetStatus")]
        [Produces("application/json")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.Forbidden)]
        public async Task<UserInfoDto> SetStatusAsync([FromForm] int id, [FromForm] string newStatus, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new SetUserStatusCommand(id, newStatus), cancellationToken);
            return _mapper.Map<UserInfoDto>(result);
        }
    }
}
