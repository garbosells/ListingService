using System;
using System.Collections.Generic;
using System.Linq;
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

        public CreateEtsyListingRequest GetEtsyListingRequest(Item item)
        {
            CreateEtsyListingRequest result = null;
            try
            {
                var isVintage = item.generalItemAttributes.era.attributeRecommendationId != 12;
                var shippingTemplateId = context.defaults.FirstOrDefault().shippingTemplateId;
                var category = context.categories.FirstOrDefault(c => c.isVintage == isVintage && c.garboSellsSubcategoryId == item.subcategoryId);
                var image_id = context.categories.FirstOrDefault(c => c.garboSellsSubcategoryId == item.subcategoryId)?.photoId;

                result = new CreateEtsyListingRequest(shippingTemplateId);

                result.image_ids = new long[] { long.Parse(image_id) };
                result.taxonomy_id = long.Parse(category.etsyCategory);
                result.title = item.shortDescription;
                result.description = GetProductDescription(item.longDescription, item.measurements);
                result.price = float.Parse(item.price);
                result.materials = GetMaterials(item.generalItemAttributes.material);
                result.when_made = GetWhenMade(item.generalItemAttributes.era);

            } catch (Exception ex)
            {
                return null;
            }
            return result;
        }

        private string GetWhenMade(ItemAttribute eraAttribute)
        {
            var era = context.eras.FirstOrDefault(e => e.Id == eraAttribute.attributeRecommendationId);
            return era.Description;
        }

        private string[] GetMaterials(ItemAttribute materialAttribute)
        {
            var material = context.materials.FirstOrDefault(m => m.Id == materialAttribute.attributeRecommendationId);
            return new string[] { material.Description };
        }

        private string GetProductDescription(string longDescription, List<ItemMeasurement> measurements)
        {
            string description = longDescription;
            if (measurements != null && measurements.Count > 0)
            {
                description += GetMeasurementsText(measurements);
            }

            var disclaimer = "<br /><br />PLEASE ASK ALL QUESTIONS PRIOR TO PURCHASING.<br /><br />UNLESS GREATLY MISREPRESENTED, A 10% RESTOCKING FEE APPLIES TO ANY RETURNS." +
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
                measurementsDescription += $"<br />{measurementTemplate.Description} {measurementTemplate.Hint}: {measurement.itemMeasurementValue}&#8243";
            });
            return measurementsDescription;
        }

        public AddEtsyInventoryAttributesRequest GetEtsyInventoryAttributesRequest(Item item, long taxonomyId)
        {
            try
            {
                var result = new AddEtsyInventoryAttributesRequest();
                var colors = context.colors;
                var generalAttributes = item.generalItemAttributes;

                if(item.size != null)
                {
                    result.etsyAttributes.Add(GetSize(item.size, taxonomyId));
                }

                if (generalAttributes.primaryColor != null)
                {
                    var colorValue = colors.FirstOrDefault(c => c.garbosellsColorId == generalAttributes.primaryColor.attributeRecommendationId)?.etsyColorId;
                    if (colorValue != null)
                        result.AddAttribute(200, colorValue);
                }

                if (generalAttributes.secondaryColor != null)
                {
                    var colorValue = colors.FirstOrDefault(c => c.garbosellsColorId == generalAttributes.secondaryColor.attributeRecommendationId)?.etsyColorId;
                    if (colorValue != null)
                        result.AddAttribute(52047899002, colorValue);
                }

                var attributes = item.attributes;
                var etsyAttributes = GetAttributes(attributes);
                if (etsyAttributes.Count > 0)
                    result.etsyAttributes.AddRange(etsyAttributes);
                return result;
            } catch (Exception ex)
            {
                throw new Exception("Problem adding attributes to Etsy item: " + ex.Message);
            }
        }

        public EtsyAttribute GetSize(ItemSize size, long taxonomyid)
        {
            var category = context.categories.FirstOrDefault(c => c.etsyCategory == taxonomyid.ToString());
            var result = new EtsyAttribute();

            if(size.sizeTypeId == 3)
            {
                //one-size logic
            }

            if(size.sizeTypeId != 1 && (bool) category.usLetterSizeOnly)
            {
                size = MapToUSLetter(size);
            }

            var sizeType = GetSizeType(size.sizeTypeId, category);
            var sizeValueId = GetSizeValueId(size.sizeValueId, sizeType.etsyScaleId, sizeType);
            var scaleId = sizeType.etsyScaleId;
            return new EtsyAttribute
            {
                scaleId = scaleId,
                attributeId = long.Parse(sizeType.estyPropertyId),
                valueId = sizeValueId
            };

        }

        private ItemSize MapToUSLetter(ItemSize size)
        {
            size.sizeTypeId = 1;
            var sizeValueId = size.sizeValueId;
            //XS
            if (sizeValueId <= 9)
            {
                size.sizeValueId = 0;
            }
            //S
            if (sizeValueId > 9 && sizeValueId <= 15)
            {
                size.sizeValueId = 1;
            }
            //M
            if (sizeValueId > 15 && sizeValueId <= 21)
            {
                size.sizeValueId = 2;
            }
            //L
            if (sizeValueId > 21 && sizeValueId <= 27)
            {
                size.sizeValueId = 3;
            }
            //XL
            if (sizeValueId > 27 && sizeValueId <= 31)
            {
                size.sizeValueId = 4;
            }
            //2XL
            if (sizeValueId > 31 && sizeValueId <= 34)
            {
                size.sizeValueId = 5;
            }
            //3XL
            if (sizeValueId > 34)
            {
                size.sizeValueId = 6;
            }
            return size;
        }

        private SizeType GetSizeType(long garbosellsSizeTypeId, Category category)
        {
            var sizeType = context.sizeTypes.FirstOrDefault(s => s.garbosellsSizeTypeId == garbosellsSizeTypeId
            && s.garbosellsSubcategoryId == category.garboSellsSubcategoryId);
            return sizeType;
        }

        private long GetSizeValueId(long garbosellsSizeValueId, long etsyScaleId, SizeType sizeType)
        {
            //US Women's numeric
            if(sizeType.garbosellsSizeTypeId == 0)
            {
                return context.numericSizes.FirstOrDefault(s => s.garbosellsSizeValueId == garbosellsSizeValueId).etsyUSValueId;
            }

            //UK Women's numeric
            if(sizeType.garbosellsSizeTypeId == 2)
            {
                return context.numericSizes.FirstOrDefault(s => s.garbosellsSizeValueId == garbosellsSizeValueId).etsyUKValueId;
            }

            //US Letter mens/womens/unisex
            return context.letterSizes.FirstOrDefault(s => s.garbosellsSizeValueId == garbosellsSizeValueId && s.etsyScaleId == sizeType.etsyScaleId).etsySizeValueId;
        }

        private EtsyAttribute GetAttribute(ItemAttribute itemAttribute)
        {
            var etsyProperty = context.properties.FirstOrDefault(p => p.garbosellsAttributeId == itemAttribute.subcategoryAttributeId);
            if(etsyProperty != null)
            {
                var etsyPropertyValue = context.propertyValues.FirstOrDefault(v => v.garbosellsRecommendationId == itemAttribute.attributeRecommendationId);
                if (etsyPropertyValue != null)
                    return new EtsyAttribute
                    {
                        attributeId = long.Parse(etsyProperty.etsyPropertyId),
                        valueId = long.Parse(etsyPropertyValue.etsyPropertyValueId)
                    };
            } 
            
            return null;
        }

        private List<EtsyAttribute> GetAttributes(List<ItemAttribute> itemAttributes)
        {
            var result = new List<EtsyAttribute>();
            foreach(var itemAttribute in itemAttributes)
            {
                var newAttribute = GetAttribute(itemAttribute);
                if(newAttribute != null)
                    result.Add(newAttribute);
            }
            return result;
        }
    }
}
