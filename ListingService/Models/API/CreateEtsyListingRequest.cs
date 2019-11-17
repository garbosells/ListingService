using System;
namespace ListingService.Models.API
{
    public class CreateEtsyListingRequest
    {
        public int quantity { get; set; } 
        public string title { get; set; } 
        public string description { get; set; } 
        public float price { get; set; } 
        public string[] materials { get; set; } 
        public long shipping_template_id { get; set; }
        public string who_made { get; set; } 
        public bool is_supply { get; set; }  
        public string when_made { get; set; }
        public string state { get; set; } 
        public long taxonomy_id { get; set; }
        public long[] image_ids { get; set; }

        public CreateEtsyListingRequest(string shippingTemplateId)
        {
            quantity = 1;
            who_made = "someone_else";
            is_supply = false;
            shipping_template_id = long.Parse(shippingTemplateId);
            state = "active"; //have to create a draft in order to edit the product and add attributes
        }
    }
}
