namespace RealEstate.Application.DTOs.Owner
{
    /// <summary>
    /// Represents a data transfer object (DTO) for creating an owner.
    /// </summary>
    public class CreateOwnerDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
    }
}
