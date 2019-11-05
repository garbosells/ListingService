using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EbayDatabase
{
    public class AspectHasRecommendation
    {
        [Column("garbosells_subcategory_attribute_recommendation_id")]
        [Key]
        public long RecommendationId { get; set; }
        [Column("aspect_id")]
        public long AspectId { get; set; }
        [Column("ebay_aspect_recommendation_description")]
        public string Description { get; set; }
        public Aspect Aspect { get; set; }
    }
}