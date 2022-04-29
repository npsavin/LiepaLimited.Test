using System;
using System.Threading;
using System.Threading.Tasks;
using LiepaLimited.Test.Application.Exceptions;
using LiepaLimited.Test.Database.Repositories;
using LiepaLimited.Test.Domain;
using MediatR;

namespace LiepaLimited.Test.Application.Command
{
    public class RemoveUserCommand: ICommand<UserInfo>
    {
        public int Id{ get; }

        public RemoveUserCommand(int id)
        {
            Id = id;
        }

    }

    internal class DeleteUserCommandHandler : IRequestHandler<RemoveUserCommand, UserInfo>
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public DeleteUserCommandHandler(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository ?? throw new ArgumentNullException(nameof(userInfoRepository));
        }

        public async Task<UserInfo> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userInfoRepository.AnyAsync(request.Id, cancellationToken))
                throw new UserExistException($"User with Id {request.Id} not found");

            return await _userInfoRepository.RemoveAsync(request.Id, cancellationToken);
        }
    }
}
