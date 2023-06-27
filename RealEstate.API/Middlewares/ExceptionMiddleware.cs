using Newtonsoft.Json;
using RealEstate.API.Models;
using System.Net;

namespace RealEstate.API.Middlewares
{
    /// <summary>
    /// Represents a middleware for handling exceptions in the RealEstateAPI.
    /// </summary>
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="logger">The logger to log exception details.</param>
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware asynchronously to handle exceptions in the request pipeline.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="next">The delegate representing the next middleware in the pipeline.</param>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                _logger.LogInformation("Action execution started: {Method} - {ActionName}", context.Request.Method, context.Request);
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
