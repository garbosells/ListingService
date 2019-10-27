﻿using System;
using System.Collections.Generic;
using GarboSellsClasses.Models;
using ListingService.Database;

namespace ListingService.Models.EbayClasses
{
    public class EbayInventoryItem
    {
        public Availability availability { get; set; }
        public int condition { get; set; }
        public Product product { get; set; }

        public EbayInventoryItem(Product product)
        {
            this.availability = new Availability(1); //only offer 1 item by default
            this.condition = 3000; //default to "Excellent" condition for now
            this.product = product;
        }
    }
}
