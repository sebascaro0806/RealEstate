namespace RealEstate.Application.DTOs.BuildingProperty
{
    /// <summary>
    /// Represents the filter criteria for querying building properties.
    /// </summary>
    public class BuildingPropertyFilterDTO
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? CodeInternal { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingPropertyFilterDTO"/> class.
        /// </summary>
        public BuildingPropertyFilterDTO()
        {
            PageNumber = 1;
            PageSize = 10;
        }
    }
}
