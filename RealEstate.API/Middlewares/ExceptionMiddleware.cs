using Newtonsoft.Json;
using RealEstate.API.Models;
using System.Net;

namespace RealEstate.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                _logger.LogInformation("Action execution started: {ActionName}", context.Request.Method);
                await next(context);
                _logger.LogInformation("Action execution completed: {ActionName}", context.Request.Method);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred in the RealEstateAPI");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new Error
                {
                    Message = "An error occurred in the RealEstateAPI"
                };

                var json = JsonConvert.SerializeObject(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
