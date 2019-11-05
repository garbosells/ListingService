using System;
using ListingService.Models.EbayClasses;

namespace ListingService.Models
{
    public class EbayProductWrapper
    {
        public Product product { get; set; }
        public long ebayCategoryId { get; set; }
    }
}
