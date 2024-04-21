using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Week4Lab.Utilities
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new UnauthorizedObjectResult("The user does not have enough permissions to create this type of account, contact an administrator.");
                context.ExceptionHandled = true;
            }
        }
    }
}
