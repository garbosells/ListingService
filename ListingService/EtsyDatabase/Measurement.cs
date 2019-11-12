using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EtsyDatabase
{
    public class Measurement
    {
        [Column("measurement_id")]
        public long Id { get; set; }
        [Column("measurement_description")]
        public string Description { get; set; }
        [Column("measurement_hint")]
        public string Hint { get; set; }
    }
}
