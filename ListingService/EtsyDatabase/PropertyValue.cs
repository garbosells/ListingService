using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EtsyDatabase
{
    public class PropertyValue
    {
        [Key]
        [Column("garbosells_recommendation_id")]
        public long garbosellsRecommendationId { get; set; }
        [Column("etsy_property_value_id")]
        public string etsyPropertyValueId { get; set; }
        [Column("description")]
        public string description { get; set; }
    }
}
