using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EbayDatabase
{
    public class Style
    {
        [Key]
        [Column("garbosells_subcategory_id")]
        public long garbosellsSubcategoryId { get; set; }
        [Column("style")]
        public string style { get; set; }
    }
}
