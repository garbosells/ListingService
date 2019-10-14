using System;
namespace ListingService.Models.EbayClasses
{
    public class EbayProduct
    {
        public Availability availability { get; set; }

        public EbayProduct()
        {
            this.availability = new Availability(1);
        }
    }
}
