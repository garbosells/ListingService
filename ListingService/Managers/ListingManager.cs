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
            var product = GenerateProduct(item);

            return new EbayInventoryItem(product);
        }

        private Product GenerateProduct(Item item)
        {
            var product = new Product();
            //determine if the thing is vintage
            //if eraId =/= 12, it is vintage!
            var isVintage = item.generalItemAttributes.era.attributeRecommendationId != 12;

            //then get the ebay category
            var category = context.categories
                .Where(c => c.garboSellsSubcategoryId == item.subcategoryId && c.isVintage == isVintage)
                .Include(c => c.categoryHasAspects)
                .ThenInclude(c => c.aspect)
                .ThenInclude(a => a.aspectHasRecommendations)
                                .ToList()
                                .FirstOrDefault();
            var aspects = category.categoryHasAspects.Select(c => { return c.aspect; }).ToList();
            product.aspects = GetProductAspects(aspects, item.attributes);


            //set size as an aspect using description from size_aspect_name in row
            //'Color' aspect = primaryColor
            //set 'Material' aspect
            //iterate over general aspects
            //set title=shortDescription
            //set description=longDescription
            //Add measurements to description
            //Add disclaimer to description
            //Use html tags!
            return product;
        }


        private Dictionary<string, string[]> GetProductAspects(List<Aspect> aspects, List<ItemAttribute> attributes)
        {
            var productAspects = new Dictionary<string, string[]>();
            aspects.ForEach(aspect =>
            {
                if (aspect.useDefault)
                {
                    productAspects.Add(aspect.aspectName, new string[] { aspect.defaultValue });
                }
                else
                {
                    string value = string.Empty;
                    var itemAttribute = attributes.Find(s => s.subcategoryAttributeId == aspect.garbosellsSubcategoryAttributeId);
                    if (itemAttribute != null)
                    {
                        if (string.IsNullOrEmpty(itemAttribute.itemAttributeValue))
                        {
                            var recommendation = aspect.aspectHasRecommendations.FirstOrDefault(a => a.RecommendationId == itemAttribute.attributeRecommendationId);
                            value = recommendation?.Description;
                        }
                        else
                        {
                            value = itemAttribute.itemAttributeValue;
                        }
                        if (!string.IsNullOrEmpty(value))
                        {
                            productAspects.Add(aspect.aspectName, new string[] { value });
                        }
                    }
                }
            });
            return productAspects;
        }
    }
}
