using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EtsyDatabase
{
    public class LetterSize
    {
        [Column("size")]
        public string letterSize { get; set; }
        [Column("etsy_scale_id")]
        public long etsyScaleId { get; set; }
        [Column("etsy_size_value_id")]
        public long etsySizeValueId { get; set; }
        [Column("garbosells_size_value_id")]
        public long garbosellsSizeValueId { get; set; }
    }
}
