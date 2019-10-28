using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.Database
{
    public class Material
    {
        [Column("material_id")]
        public long Id { get; set; }
        [Column("material_description")]
        public string Description { get; set; }
    }
}
