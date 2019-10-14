using System;
using GarboSellsClasses.Models;
using ListingService.Models.EbayClasses;

namespace ListingService.Managers
{
    public class ListingManager
    {
        public EbayInventoryItem MapItemToEbayProduct(Item item)
        {
            //determine if the thing is vintage, then get the ebay category
            //if eraId =/= 12, it is vintage!
            var isVintage = item.generalItemAttributes.era.itemAttributeId == 12;

           //set size as an aspect using description from size_aspect_name in row
           //'Color' aspect = primaryColor
           //set 'Material' aspect
           //iterate over general aspects
           //set title=shortDescription
           //set description=longDescription
           //Add measurements to description
           //Add disclaimer to description
           //Use html tags!
            return new EbayInventoryItem();
        }
    }
}
