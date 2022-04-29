using System;
using System.Net;

namespace LiepaLimited.Test.Application.Exceptions
{
    public class BaseException: Exception
    {
        public int ErrorId { get; }
        public string Message { get;  }
        public HttpStatusCode StatusCode { get; }
        public BaseException(ErrorCode errorCode, string message, HttpStatusCode statusCode) : base(message)
        {
            ErrorId = (int) errorCode;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
