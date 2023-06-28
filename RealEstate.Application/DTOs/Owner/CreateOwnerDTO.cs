using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.Owner
{
    /// <summary>
    /// Represents a data transfer object (DTO) for creating an owner.
    /// </summary>
    public class CreateOwnerDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format. The expected format is yyyy/MM/dd.")]
        public DateTime Birthday { get; set; }
    }
}
