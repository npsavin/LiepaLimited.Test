using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LiepaLimited.Test.Application.Dto;
using LiepaLimited.Test.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace LiepaLimited.Test.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BaseException ex)
            {
                await HandleExceptionAsync(httpContext, ex.ErrorId, ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, (int)ErrorCode.InternalError, ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, int errorId, string message, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = context.Request.ContentType;
            var errorDto = new ErrorResponseDto(errorId, message);
            if (context.Response.ContentType == "application/xml")
            {
                await using var writer = new StringWriter();
                new XmlSerializer(errorDto.GetType()).Serialize(writer, errorDto);
                var result = writer.ToString();
                context.Response.ContentLength = result.Length;
                await context.Response.WriteAsync(result);
            }
            else
            {
                await context.Response.WriteAsJsonAsync(errorDto);
            }
        }
       
    }
}
