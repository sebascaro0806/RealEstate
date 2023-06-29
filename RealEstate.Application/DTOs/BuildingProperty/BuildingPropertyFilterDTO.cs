namespace RealEstate.Application.DTOs.BuildingProperty
{ 
    public class BuildingPropertyFilterDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
    }
}
