using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models;

namespace RealEstate.API.Filters
{
    public class ValidateFileAttribute : ActionFilterAttribute
    {
        private readonly int _maxFileSize; 
        private readonly string[] _allowedExtensions;
        private readonly int _maxFileSizeMB;
        private const int bytesInMegabyte = 1048576;

        public ValidateFileAttribute(int maxFileSize, params string[] allowedExtensions)
        {
            _maxFileSize = maxFileSize;
            _allowedExtensions = allowedExtensions;
            _maxFileSizeMB = (int)(maxFileSize / bytesInMegabyte);
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.HasFormContentType)
            {
                AddValidationError(context, "Invalid request.");
                return;
            }

            var file = context.HttpContext.Request.Form.Files.FirstOrDefault();

            if (file == null || file.Length == 0)
            {
                AddValidationError(context, "No file uploaded.");
                return;
            }

            if (!IsValidImage(file))
            {
                AddValidationError(context, $"Invalid image format. Allowed formats are: { string.Join(", ", _allowedExtensions) }");
                return;
            }

            if (!IsFileSizeValid(file))
            {
                AddValidationError(context, $"File size exceeds the limit. Maximum file size allowed is { _maxFileSizeMB } MB.");
                return;
            }

            await next();
        }

        private void AddValidationError(ActionExecutingContext context, string errorMessage)
        {
  
            context.Result = new BadRequestObjectResult(
                new ErrorResponse
                {
                    Message = "Model validation error",
                    Errors = new List<string> { errorMessage }
                });
        }

        private bool IsValidImage(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return _allowedExtensions.Contains(fileExtension);
        }

        private bool IsFileSizeValid(IFormFile file)
        {
            return file.Length <= _maxFileSize;
        }
    }
}
