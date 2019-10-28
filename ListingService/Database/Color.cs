using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.Database
{
    public class Color
    {
        [Column("color_id")]
        public long Id { get; set; }
        [Column("color_description")]
        public string Description { get; set; }
    }
}
