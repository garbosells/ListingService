using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EbayDatabase
{
    public class Size
    {
        [Column("garbosells_size_value_id")]
        public long Id { get; set; }
        [Column("size_value")]
        public string Value { get; set; }
    }
}
