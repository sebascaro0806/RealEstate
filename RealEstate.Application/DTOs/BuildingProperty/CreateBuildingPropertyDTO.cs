namespace RealEstate.Application.DTOs.BuildingProperty
{
    public class CreateBuildingPropertyDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public Guid OwnerId { get; set; }
    }
}
