using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EtsyDatabase
{
    public class Color
    {
        [Key]
        [Column("garbosells_color_id")]
        public long garbosellsColorId { get; set; }
        [Column("etsy_color_id")]
        public long etsyColorId { get; set; }
    }
}
