using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EtsyDatabase
{
    public class SizeType
    {
        [Column("garbosells_size_type_id")]
        public long garbosellsSizeTypeId { get; set; }
        [Column("etsy_size_type_id")]
        public long etsyScaleId { get; set; }
        [Column("is_numeric")]
        public bool isNumeric { get; set; }
        [Column("garbosells_subcategory_id")]
        public long garbosellsSubcategoryId { get; set; }
        [Column("etsy_property_id")]
        public string estyPropertyId { get; set; }
        [Column("description")]
        public string description { get; set; }
    }
}
