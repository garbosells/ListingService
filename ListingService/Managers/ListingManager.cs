using System;
using System.Collections.Generic;
using System.Linq;
using GarboSellsClasses.Models;
using ListingService.Database;
using ListingService.Models;
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

        public EbayInventoryItemWrapper MapItemToEbayInventoryItem(Item item)
        {
            var productWrapper = GenerateProduct(item);
            var ebayInventoryItemWrapper = new EbayInventoryItemWrapper(productWrapper);
            return ebayInventoryItemWrapper;
        }

        private EbayProductWrapper GenerateProduct(Item item)
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
            product.aspects = GetProductAspects(category, item);

            //set title=shortDescription
            product.title = item.shortDescription;
            //set description=longDescription
            product.description = GetProductDescription(item.longDescription, item.measurements);

            return new EbayProductWrapper {
                    product = product,
                    ebayCategoryId = category.ebayCategoryId
            };
        }

        private string GetProductDescription(string longDescription, List<ItemMeasurement> measurements)
        {
            string description = longDescription;
            if (measurements != null && measurements.Count > 0)
            {
                description += GetMeasurementsText(measurements);
            }

            var disclaimer = "<br /><br />PLEASE ASK ALL QUESTIONS PRIOR TO PURCHASING.<br /><br />UNLESS GREATLY MISREPRESENTED, A 10 % RESTOCKING FEE APPLIES TO ANY RETURNS." +
"<br /><br />DISCLAIMER ON CLOTHING:<br />UNLESS NOTED, ALL CLOTHING IS PRE - OWNED or LIGHTLY LOVED.Special effort is made to seek out the best quality items, " +
"but due to previous use, some flaws may exist.Flaws may include, but are not limited to a loose string(s), fading(this applies to vintage clothes), or " +
"a small scuff. Sherman Garbo Sells makes an effort to disclose any flaws. Customer service is a top priority and questions will be answered quickly and accurately." +
"<br /><br />Please be sure to view all photos(taken with and without flash) and ask questions. Thanks for looking!!!";

            description += disclaimer;

            return description;
        }

        private string GetMeasurementsText(List<ItemMeasurement> measurements)
        {
            string measurementsDescription = "<br /><br />Approximate Measurements:";
            measurements.ForEach(measurement =>
            {
                var measurementTemplate = context.measurements.FirstOrDefault(m => m.Id == measurement.categoryMeasurementId);
                measurementsDescription += $"<br />{measurementTemplate.Description} ({measurementTemplate.Hint}): {measurement.itemMeasurementValue}&#8220";
            });
            return measurementsDescription;
        }

        private Dictionary<string, string[]> GetProductAspects(Category category, Item item)
        {
            var attributes = item.attributes;
            var aspects = category.categoryHasAspects.Select(c => { return c.aspect; }).ToList();
            var productAspects = GetGeneralProductAspects(category, item);

            aspects.ForEach(aspect =>
            {
                //aspect has a default value that should be used every time this product is generated
                if (aspect.useDefault)
                {
                    productAspects.Add(aspect.aspectName, new string[] { aspect.defaultValue });
                }
                //aspect has some other value set by the user
                else
                {
                    string value = string.Empty;
                    var itemAttribute = attributes.Find(s => s.subcategoryAttributeId == aspect.garbosellsSubcategoryAttributeId);
                    if (itemAttribute != null)
                    {
                        //if there is not a custom value selected
                        if (string.IsNullOrEmpty(itemAttribute.itemAttributeValue))
                        {
                            var recommendation = aspect.aspectHasRecommendations.FirstOrDefault(a => a.RecommendationId == itemAttribute.attributeRecommendationId);
                            value = recommendation?.Description;
                        }
                        //use custom value
                        else
                        {
                            value = itemAttribute.itemAttributeValue;
                        }
                        //add the aspect if its value is not null or empty
                        if (!string.IsNullOrEmpty(value))
                        {
                            productAspects.Add(aspect.aspectName, new string[] { value });
                        }
                    }
                }
            });
            return productAspects;
        }

        private Dictionary<string, string[]> GetGeneralProductAspects(Category category, Item item)
        {
            var aspects = new Dictionary<string, string[]>();
            //set size as an aspect using description from size_aspect_name in row
            var size = context.sizes.FirstOrDefault(s => item.size.sizeValueId == s.Id);
            aspects.Add(category.sizeAspectName, new string[] { size.Value });

            //'Color' aspect = primaryColor
            var color = context.colors.FirstOrDefault(c => c.Id == item.generalItemAttributes.primaryColor.attributeRecommendationId);
            aspects.Add("Color", new string[] { color.Description });

            if (item.generalItemAttributes.material.attributeRecommendationId != null)
            {
                //set 'Material' aspect
                var material = context.materials.FirstOrDefault(m => m.Id == item.generalItemAttributes.material.attributeRecommendationId);
                aspects.Add("Material", new string[] { material.Description });
            }


            //only add Vintage aspect if it is vintage
            if (category.isVintage)
            {
                aspects.Add("Vintage", new string[] { "Yes" });
            }

            return aspects;
        }
    }
}
