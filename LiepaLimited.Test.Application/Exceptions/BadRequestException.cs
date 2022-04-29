using System.Net;

namespace LiepaLimited.Test.Application.Exceptions
{
    public class BadRequestException: BaseException
    {
        public BadRequestException(string message) : base(ErrorCode.BadRequest, message, HttpStatusCode.BadRequest)
        {
        }
    }
}
