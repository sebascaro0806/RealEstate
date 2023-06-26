using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Models
{
    public class BuildingProperty
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Double Price { get; set; }

        public string CodeInternal { get; set; }

        public int Year { get; set; } 
    }
}
