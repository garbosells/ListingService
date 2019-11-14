using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EtsyDatabase
{
    public class NumericSize
    {
        [Key]
        [Column("garbosells_size_value_id")]
        public long garbosellsSizeValueId { get; set; }
        [Column("etsy_us_value_id")]
        public long etsyUSValueId { get; set; }
        [Column("etsy_uk_value_id")]
        public long etsyUKValueId { get; set; }
    }
}
