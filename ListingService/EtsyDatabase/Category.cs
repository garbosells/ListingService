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
        [Column("esty_category")]
        public string etsyCategory { get; set; }
    }
}