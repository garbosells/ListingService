using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.EtsyDatabase
{
    public class Defaults
    {
        [Key]
        public int id { get; set; }
        [Column("shipping_template_id")]
        public string shippingTemplateId { get; set; }
    }
}
