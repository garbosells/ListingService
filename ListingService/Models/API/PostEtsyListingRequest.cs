using System;
namespace ListingService.Models.API
{
    public class PostEtsyListingRequest
    {
        public CreateEtsyListingRequest createListingRequest { get; set; }
        public AddEtsyInventoryAttributesRequest addInventoryAttributesRequest { get; set; }
        public PostEtsyListingRequest()
        {
        }
    }
}
