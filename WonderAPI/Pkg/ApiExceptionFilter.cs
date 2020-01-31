using Microsoft.AspNetCore.Mvc.Filters;

namespace WonderAPI.Pkg
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            object validationResult;
            if (context.Exception is AppException)
            {
                context.HttpContext.Response.StatusCode = 400;

                // handle explicit 'known' API errors
                var ex = context.Exception as AppException;
                context.Exception = null;
                validationResult = ex.ValidationResult;
                if (validationResult == null)
                {
                    validationResult = new
                    {
                        errorMessage = ex.Message
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

                validationResult = new ValidationResult("Something went error");


                // handle logging here
            }

            // always return a JSON result
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(validationResult);

            base.OnException(context);
        }
    }
}
