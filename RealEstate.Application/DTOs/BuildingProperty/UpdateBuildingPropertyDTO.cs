using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.BuildingProperty
{
    /// <summary>
    /// Represents a data transfer object (DTO) for updating a building property.
    /// </summary>
    public class UpdateBuildingPropertyDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public double Price { get; set; }

        [RegularExpression(@"^\w{3}-\w{3}$", ErrorMessage = "CodeInternal must have the format XXX-XXX")]
        public string CodeInternal { get; set; }

        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
        public int Year { get; set; }
    }
}
