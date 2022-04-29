using System.Net;

namespace LiepaLimited.Test.Application.Exceptions
{
    public class UserNotFoundException: BaseException
    {
        public UserNotFoundException(string message) : base(ErrorCode.UserNotFound, message, HttpStatusCode.NotFound)
        {
        }
    }
}
