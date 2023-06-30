using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models;

namespace RealEstate.API.Filters
{
    /// <summary>
    /// A filter attribute for validating uploaded files in a request.
    /// </summary>
    public class ValidateFileAttribute : ActionFilterAttribute
    {
        private readonly int _maxFileSize; 
        private readonly string[] _allowedExtensions;
        private readonly int _maxFileSizeMB;
        private const int bytesInMegabyte = 1048576;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateFileAttribute"/> class.
        /// </summary>
        /// <param name="maxFileSize">The maximum allowed file size in bytes.</param>
        /// <param name="allowedExtensions">The allowed file extensions.</param>
        public ValidateFileAttribute(int maxFileSize, params string[] allowedExtensions)
        {
            _maxFileSize = maxFileSize;
            _allowedExtensions = allowedExtensions;
            _maxFileSizeMB = (int)(maxFileSize / bytesInMegabyte);
        }

        /// <summary>
        /// Validates the uploaded file in the request.
        /// </summary>
        /// <param name="context">The context of the action being executed.</param>
        /// <param name="next">The delegate representing the next action filter or the action itself.</param>
        /// <returns>A task that represents the asynchronous execution of the action filter.</returns>
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

        /// <summary>
        /// Adds a validation error to the action execution context with an error message.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        /// <param name="errorMessage">The error message.</param>
        private void AddValidationError(ActionExecutingContext context, string errorMessage)
        {
  
            context.Result = new BadRequestObjectResult(
                new ErrorResponse
                {
                    Message = "Model validation error",
                    Errors = new List<string> { errorMessage }
                });
        }

        /// <summary>
        /// Checks if the file is a valid image based on its extension.
        /// </summary>
        /// <param name="file">The file to validate.</param>
        /// <returns><c>true</c> if the file is a valid image; otherwise, <c>false</c>.</returns>
        private bool IsValidImage(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return _allowedExtensions.Contains(fileExtension);
        }

        /// <summary>
        /// Checks if the file size is valid.
        /// </summary>
        /// <param name="file">The file to validate.</param>
        /// <returns><c>true</c> if the file size is valid; otherwise, <c>false</c>.</returns>
        private bool IsFileSizeValid(IFormFile file)
        {
            return file.Length <= _maxFileSize;
        }
    }
}
