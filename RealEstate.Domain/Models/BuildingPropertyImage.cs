using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Models
{
    public class BuildingPropertyImage
    {
        [Key]
        public Guid Id { get; set; }

        public byte[] ImageData { get; set; }

        [ForeignKey("BuildingProperty")]
        public Guid BuildingPropertyId { get; set; }

        public virtual BuildingProperty BuildingProperty { get; set; }
    }
}
