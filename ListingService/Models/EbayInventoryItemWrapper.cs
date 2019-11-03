using System;
using ListingService.Models.EbayClasses;

namespace ListingService.Models
{
    public class EbayInventoryItemWrapper
    {
        public EbayInventoryItem ebayInventoryItem { get; set; }
        public long ebayCategoryId { get; set; }

        public EbayInventoryItemWrapper(EbayProductWrapper productWrapper)
        {
            this.ebayInventoryItem = new EbayInventoryItem(productWrapper.product);
            this.ebayCategoryId = productWrapper.ebayCategoryId;
        }
    }
}
