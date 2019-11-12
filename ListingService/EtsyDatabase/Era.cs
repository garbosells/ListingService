using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EtsyDatabase
{
    public class Era
    {
        [Key]
        [Column("era_id")]
        public long Id { get; set; }
        [Column("era_description")]
        public string Description { get; set; }
    }
}
