using System;
using System.Threading;
using System.Threading.Tasks;
using LiepaLimited.Test.Application.Exceptions;
using LiepaLimited.Test.Database.Repositories;
using LiepaLimited.Test.Domain;
using MediatR;

namespace LiepaLimited.Test.Application.Command
{
    public class CreateUserCommand: ICommand<UserInfo>
    {
        public UserInfo UserInfo { get; }

        public CreateUserCommand(UserInfo userInfo)
        {
            UserInfo = userInfo;
        }

    }

    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserInfo>
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public CreateUserCommandHandler(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository ?? throw new ArgumentNullException(nameof(userInfoRepository));
        }

        public async Task<UserInfo> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserInfo == null)
                throw new BadRequestException("User can't be null");

            if (await _userInfoRepository.AnyAsync(request.UserInfo.Id, cancellationToken))
                throw new UserExistException($"User with id {request.UserInfo.Id} already exist");

            var user = await _userInfoRepository.AddAsync(request.UserInfo, cancellationToken);
            return user;
        }
    }
}
