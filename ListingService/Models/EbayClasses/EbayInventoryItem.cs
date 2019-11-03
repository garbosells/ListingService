using System;
using System.Collections.Generic;
using GarboSellsClasses.Models;
using ListingService.Database;

namespace ListingService.Models.EbayClasses
{
    public class EbayInventoryItem
    {
        public Availability availability { get; set; }
        public string condition { get; set; }
        public Product product { get; set; }

        public EbayInventoryItem(Product product)
        {
            this.availability = new Availability(1); //only offer 1 item by default
            this.condition = "USED_EXCELLENT"; 
            this.product = product;
        }
    }
}
