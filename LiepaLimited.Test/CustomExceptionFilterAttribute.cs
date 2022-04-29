using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiepaLimited.Test.Application.Dto;
using LiepaLimited.Test.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LiepaLimited.Test
{
//    public class CustomExceptionFilterAttribute: ExceptionFilterAttribute
//    {
//        public override void OnException(ExceptionContext context)
//        {
//            base.OnException(context);

//            if (context.Exception is BaseException baseException)
//            {
//                context.Result = new NotFoundResult(new ErrorResponseDto(baseException.ErrorId, baseException.Message));
//            }
//            else
//            {
//                context.Result = new ErrorResponseDto((int)ErrorCode.InternalError, context.Exception.Message);
//            }
//        }
//    }
}
