using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Models
{
    /// <summary>
    /// Represents a building property.
    /// </summary>
    public class BuildingProperty
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Double Price { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodeInternal { get; set; }

        public int Year { get; set; }

        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual ICollection<BuildingPropertyImage> BuildingPropertiesImages { get; set; }

    }
}
