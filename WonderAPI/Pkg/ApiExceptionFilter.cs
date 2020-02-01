using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace WonderAPI.Pkg
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            object exceptionResult;

            if (context.Exception is ValidationException || context.Exception is AppException)
            {
                // handle explicit 'known' API errors
                context.HttpContext.Response.StatusCode = 400;

                if (context.Exception is ValidationException)
                {
                    var ex = context.Exception as ValidationException;
                    exceptionResult = ex.ValidationResult;
                }
                else
                {
                    var ex = context.Exception as AppException;
                    exceptionResult = new
                    {
                        message = ex.Message,
                        errors = new List<object>()
                    };
                }
            }

            else
            {
                // Unhandled errors

                context.HttpContext.Response.StatusCode = 500;
#if !DEBUG
                var msg = "An unhandled error occurred.";                
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                exceptionResult = new ValidationResult("Something went error.");


                // handle logging here
            }

            // always return a JSON result
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(exceptionResult);

            base.OnException(context);
        }
    }
}
