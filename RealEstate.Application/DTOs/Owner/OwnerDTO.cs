namespace RealEstate.Application.DTOs.Owner
{
    /// <summary>
    /// Represents a data transfer object (DTO) for an owner.
    /// </summary>
    public class OwnerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
    }
}
