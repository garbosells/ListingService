using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListingService.Database
{
    public class Defaults
    {
        [Column("company_id")]
        [Key]
        public long companyId { get; set; }
        [Column("payment_policy_id")]
        public string paymentPolicyId { get; set; }
        [Column("return_policy_id")]
        public string returnPolicyId { get; set; }
        [Column("fulfillment_policy_id")]
        public string fulfillmentPolicyId { get; set; }
        [Column("merchant_location_key")]
        public string merchantLocationKey { get; set; }
    }
}
