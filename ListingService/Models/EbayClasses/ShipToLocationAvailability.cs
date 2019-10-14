namespace ListingService.Models.EbayClasses
{
    public class ShipToLocationAvailability
    {
        public int quantity { get; set; }

        public ShipToLocationAvailability(int quantity)
        {
            this.quantity = quantity;
        }
    }
}