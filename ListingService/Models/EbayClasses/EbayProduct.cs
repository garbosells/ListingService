using System;
namespace ListingService.Models.EbayClasses
{
    public class EbayProduct
    {
        public Availability availability { get; set; }
        public int condition { get; set; }

        public EbayProduct()
        {
            this.availability = new Availability(1);
            this.condition = 3000; //default to "Excellent" condition for now
        }
    }
}
