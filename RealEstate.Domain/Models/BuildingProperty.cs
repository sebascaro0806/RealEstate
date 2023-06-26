using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual BuildingPropertyImage BuildingPropertyImage { get; set; }

    }
}
