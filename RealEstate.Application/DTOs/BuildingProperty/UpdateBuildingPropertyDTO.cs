namespace RealEstate.Application.DTOs.BuildingProperty
{
    /// <summary>
    /// Represents a data transfer object (DTO) for updating a building property.
    /// </summary>
    public class UpdateBuildingPropertyDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
    }
}
