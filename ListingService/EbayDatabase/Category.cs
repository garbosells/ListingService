using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EbayDatabase
{
    public class Category
    {
        public virtual ICollection<CategoryHasAspect> categoryHasAspects { get; set; }

        [Column("garbosells_subcategory_id")]
        public long garboSellsSubcategoryId { get; set; }
        [Column("is_vintage")]
        public bool isVintage { get; set; }
        [Column("ebay_category_id")]
        public long ebayCategoryId { get; set; }
        [Column("size_aspect_name")]
        public string sizeAspectName { get; set; }
    }
}
