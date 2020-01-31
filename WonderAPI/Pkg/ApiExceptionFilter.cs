using Microsoft.AspNetCore.Mvc.Filters;

namespace WonderAPI.Pkg
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ValidationResult validationResult;
            if (context.Exception is AppException)
            {
                // handle explicit 'known' API errors
                var ex = context.Exception as AppException;
                context.Exception = null;
                validationResult = ex.ValidationResult;

                context.HttpContext.Response.StatusCode = 400;
            }
            else
            {
                // Unhandled errors
#if !DEBUG
                var msg = "An unhandled error occurred.";                
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                validationResult = new ValidationResult("Something went error");

                context.HttpContext.Response.StatusCode = 500;

                // handle logging here
            }

            // always return a JSON result
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(validationResult);

            base.OnException(context);
        }
    }
}
