using System;
using System.Collections.Generic;

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
    }
}
