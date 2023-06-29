using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.API.Models;

namespace RealEstate.API.Filters
{
    public class ValidateGuidIdAttribute : ActionFilterAttribute
    {
        private readonly string _parameterName;

        public ValidateGuidIdAttribute(string parameterName)
        {
            _parameterName = parameterName;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!Guid.TryParse(context.ActionArguments[_parameterName]?.ToString(), out Guid id))
            {
                var response = new ErrorResponse
                {
                    Message = "Model validation error",
                    Errors = new List<string> { $"Invalid { _parameterName } Guid Format" }
                };

               context.Result = new BadRequestObjectResult(response);
               return;
            }

            await next();
        }
    }
}
