namespace ListingService.Models.EbayClasses
{
    public class Availability
    {
        public ShipToLocationAvailability shipToLocationAvailability { get; set; }

        public Availability(int quantity)
        {
            this.shipToLocationAvailability = new ShipToLocationAvailability(quantity);
        }
    }
}