using System;
using GarboSellsClasses.Models;
using ListingService.Models.EbayClasses;

namespace ListingService.Managers
{
    public class ListingManager
    {
        public EbayInventoryItem MapItemToEbayProduct(Item item)
        {
            return new EbayInventoryItem();
        }
    }
}
