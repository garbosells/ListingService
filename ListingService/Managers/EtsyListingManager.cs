using System;
using GarboSellsClasses.Models;
using ListingService.EtsyDatabase;
using ListingService.Models.API;

namespace ListingService.Managers
{
    public class EtsyListingManager
    {
        private readonly EtsyDatabaseContext context;

        public EtsyListingManager(EtsyDatabaseContext context)
        {
            this.context = context;
        }

        public PostEtsyListingRequest GetEtsyListingRequest(Item item)
        {
            PostEtsyListingRequest result = null;
            try
            {
                
            } catch (Exception ex)
            {

            }
            return result;
        }
    }
}
