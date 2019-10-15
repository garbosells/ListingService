using System;
using System.Collections.Generic;
using System.Linq;
using GarboSellsClasses.Models;
using ListingService.Database;
using ListingService.Models.EbayClasses;
using Microsoft.EntityFrameworkCore;

namespace ListingService.Managers
{
    public class ListingManager
    {
        private readonly EbayDatabaseContext context;

        public ListingManager(EbayDatabaseContext context)
        {
            this.context = context;
        }

        public EbayInventoryItem MapItemToEbayProduct(Item item)
        {
            var category = context.categories
                            .Where(c => c.garboSellsSubcategoryId == item.subcategoryId)
                            .Include(c => c.categoryHasAspects)
                            .ThenInclude(c => c.aspect)
                                            .ToList()
                                            .FirstOrDefault();
            var productAspects = category.categoryHasAspects.Select(c => { return c.aspect; }).ToList();
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
            return new EbayInventoryItem(productAspects, item);
        }
    }
}
