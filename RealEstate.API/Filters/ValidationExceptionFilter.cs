using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models;

namespace RealEstate.API.Filters
{
    public class ValidationExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Obtener los errores de validación del modelo
                var errors = context.ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Crear una respuesta de error BadRequest con los errores de validación
                var errorResponse = new Error
                {
                    Message = "Bad Request",
                    Errors = errors
                };

                // Devolver la respuesta de error BadRequest
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
