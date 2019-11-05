using System;
using ListingService.Models.EbayClasses;

namespace ListingService.Models.API
{
    public class PostEbayListingRequest
    {
        public string categoryId { get; set; }
        public string paymentPolicyId { get; set; }
        public string fulfillmentPolicyId { get; set; }
        public string returnPolicyId { get; set; }
        public string merchantLocationKey { get; set; }
        public string price;
        public EbayInventoryItem inventoryItem { get; set; }
        public PostEbayListingRequest()
        {
        }
    }
}
