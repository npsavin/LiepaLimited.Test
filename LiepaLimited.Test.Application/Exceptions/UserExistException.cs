using System.Net;

namespace LiepaLimited.Test.Application.Exceptions
{
    public class UserExistException: BaseException
    {
        public UserExistException(string message) : base(ErrorCode.UserExists, message, HttpStatusCode.BadRequest)
        {
        }
    }
}
