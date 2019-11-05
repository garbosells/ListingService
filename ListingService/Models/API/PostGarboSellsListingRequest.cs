using System;
using GarboSellsClasses.Models;

namespace ListingService.Models.API
{
    public class PostGarboSellsListingRequest
    {
        public bool postToEbay { get; set; }
        public bool postToEtsy { get; set; }
        public Listing listing { get; set; }

        public PostGarboSellsListingRequest()
        {
        }
    }
}
