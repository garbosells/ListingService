using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EbayDatabase
{
    public class Decade
    {
        [Key]
        [Column("garbosells_era_id")]
        public long garbosellsEraId { get; set; }
        [Column("ebay_decade")]
        public string decade { get; set; }
    }
}
