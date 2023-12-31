﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Models
{
    /// <summary>
    /// Represents an image associated with a building property.
    /// </summary>
    public class BuildingPropertyImage
    {
        [Key]
        public Guid Id { get; set; }

        public string Url { get; set; }

        [ForeignKey("BuildingProperty")]
        public Guid BuildingPropertyId { get; set; }

        public bool Enabled { get; set; }

        public virtual BuildingProperty BuildingProperty { get; set; }
    }
}
