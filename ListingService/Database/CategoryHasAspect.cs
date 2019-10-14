using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.Database
{
    public class CategoryHasAspect
    {
        public Category category { get; set; }
        public Aspect aspect { get; set; }

        [Column("garbosells_subcategory_id")]
        public long garbosellsSubcategoryId { get; set; }
        [Column("is_vintage")]
        public bool isVintage { get; set; }
        [Column("aspect_id")]
        public long aspectId { get; set; }
    }
}