using System;
namespace ListingService.Models.API
{
    public class PostGarboSellsListingResponse : Response
    {
        public PostListingResponse PostEbayListingResponse { get; set; }
        public PostListingResponse PostEtsyListingResponse { get; set; }
    }
}
