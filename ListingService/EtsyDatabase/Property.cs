using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EtsyDatabase
{
    public class Property
    {
        [Key]
        [Column("etsy_property_id")]
        public string etsyPropertyId { get; set; }
        [Column("description")]
        public string description { get; set; }
        [Column("garbosells_attribute_id")]
        public long? garbosellsAttributeId { get; set; }
    }
}
