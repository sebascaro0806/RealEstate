﻿using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Models
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public DateTime Birthday { get; set; }

        public virtual ICollection<BuildingProperty> BuildingProperties { get; set; }

    }
}
