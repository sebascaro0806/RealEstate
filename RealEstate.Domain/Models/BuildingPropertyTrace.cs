using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Models
{
    /// <summary>
    /// Represents a trace of a building property sale.
    /// </summary>
    public class BuildingPropertyTrace
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }

        public Double Value { get; set; }

        public Double Tax { get; set; }

        public int Year { get; set; }

        [ForeignKey("BuildingProperty")]
        public Guid BuildingPropertyId { get; set; }

        public virtual BuildingProperty BuildingProperty { get; set; }
    }
}
