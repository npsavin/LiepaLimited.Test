using System;
using System.Threading;
using System.Threading.Tasks;
using LiepaLimited.Test.Application.Exceptions;
using LiepaLimited.Test.Application.Mapping;
using LiepaLimited.Test.Database.Repositories;
using LiepaLimited.Test.Domain;
using MediatR;

namespace LiepaLimited.Test.Application.Command
{
    public class SetUserStatusCommand : ICommand<UserInfo>
    {
        public int Id { get; }
        public string Status { get; }

        public SetUserStatusCommand(int id, string status)
        {
            Id = id;
            Status = status;
        }

    }

    internal class SetUserStatusCommandHandler : IRequestHandler<SetUserStatusCommand, UserInfo>
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public SetUserStatusCommandHandler(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository ?? throw new ArgumentNullException(nameof(userInfoRepository));
        }

        public async Task<UserInfo> Handle(SetUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userInfoRepository.GetAsync(request.Id);
            if (user == null)
                throw new UserNotFoundException($"User with id {request.Id} not found");

            user.Status = request.Status.MapToStatus();
            return await _userInfoRepository.UpdateAsync(user, cancellationToken);
        }
    }
}
