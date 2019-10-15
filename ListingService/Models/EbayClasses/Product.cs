using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GarboSellsClasses.Models;
using ListingService.Database;

namespace ListingService.Models.EbayClasses
{
    public class Product
    {
        public Dictionary<string, string[]> aspects { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        private readonly string categoryDataServiceBaseUrl = "https://categorydataservice.azurewebsites.net/api/category/";

        public Product()
        {
            this.aspects = new Dictionary<string, string[]>();
        }

        public void AddAspects(List<Aspect> aspects, List<ItemAttribute> attributes)
        {
            aspects.ForEach(a =>
            {
                if(a.useDefault)
                {
                    AddAspect(a.aspectName, a.defaultValue);
                } else
                {
                    string value = string.Empty;
                    var itemAttribute = attributes.Find(s => s.subcategoryAttributeId == a.garbosellsSubcategoryAttributeId);
                    if(itemAttribute != null)
                    {
                        if (string.IsNullOrEmpty(itemAttribute.itemAttributeValue))
                        {
                            var recommendation = GetRecommendationById(itemAttribute.attributeRecommendationId).Result;
                            value = recommendation?.Description;
                        }
                        else
                        {
                            value = itemAttribute.itemAttributeValue;
                        }
                        if (!string.IsNullOrEmpty(value))
                        {
                            AddAspect(a.aspectName, value);
                        }
                    }
                }
            });
        }

        private void AddAspect(string name, string value)
        {
            this.aspects.Add(name, new string[] { value });
        }

        private async Task<Recommendation> GetRecommendationById(long? recommendationId)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync(categoryDataServiceBaseUrl + "getAttributeRecommendationById?attributeRecommendationId=" + recommendationId);
            return await result.Result.Content.ReadAsAsync<Recommendation>();
        }
    }
}
