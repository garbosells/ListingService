using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EtsyDatabase
{
    public class Category
    {
        [Column("garbosells_subcategory_id")]
        public long garboSellsSubcategoryId { get; set; }
        [Column("is_vintage")]
        public bool isVintage { get; set; }
        [Column("etsy_category")]
        public string etsyCategory { get; set; }
        [Column("photo_id")]
        public string photoId { get; set; }
        [Column("us_letter_size_only")]
        public bool? usLetterSizeOnly { get; set; }
    }
}
