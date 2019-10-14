using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.Database
{
    public class Aspect
    {
        public virtual ICollection<CategoryHasAspect> categoryHasAspects { get; set; }

        [Column("aspect_id")]
        public long aspectId { get; set; }
        [Column("garbosells_subcategory_attribute_id")]
        public long? garbosellsSubcategoryAttributeId { get; set; }
        [Column("use_default")]
        public bool useDefault { get; set; }
        [Column("required")]
        public bool isRequired { get; set; }
        [Column("default_value")]
        public string defaultValue { get; set; }
    }
}