using System.Net;

namespace RealEstate.API.Exceptions
{
    public class CustomException : Exception
    {
        public string? ErrorMessage { get; }

        public HttpStatusCode StatusCode { get; }

        public CustomException(string message, string? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorMessage = errors;
            StatusCode = statusCode;
        }
    }
}
