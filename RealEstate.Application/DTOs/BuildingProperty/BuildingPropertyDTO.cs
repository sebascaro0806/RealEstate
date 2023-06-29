namespace RealEstate.Application.DTOs.BuildingProperty
{
    /// <summary>
    /// Represents a data transfer object (DTO) for a building property.
    /// </summary>
    public class BuildingPropertyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public Guid OwnerId { get; set; }

        public ICollection<BuildingPropertyImageDTO> BuildingPropertiesImages { get; set; }
    }
}
