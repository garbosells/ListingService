using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GarboSellsClasses.Models;
using ListingService.EbayDatabase;

namespace ListingService.Models.EbayClasses
{
    public class Product
    {
        public Dictionary<string, string[]> aspects { get; set; }
        public string description { get; set; }
        public string title { get; set; }

        public Product()
        {
            this.aspects = new Dictionary<string, string[]>();
        }

        public void SetTitle(string title)
        {
            this.title = title;
        }
    }
}
