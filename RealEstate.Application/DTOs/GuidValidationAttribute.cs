
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs
{
    public class GuidValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string guidString && !Guid.TryParse(guidString, out _))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
