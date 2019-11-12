using System;
using System.Collections.Generic;

namespace ListingService.Models.API
{
    public class AddEtsyInventoryAttributesRequest
    {
        public List<EtsyAttribute> etsyAttributes { get; set; }

        public AddEtsyInventoryAttributesRequest()
        {
            etsyAttributes = new List<EtsyAttribute>();
        }

        public void AddAttribute(long attributeId, long? valueId)
        {
            if(valueId != null)
                etsyAttributes.Add(new EtsyAttribute { attributeId = attributeId,
                valueId = valueId });
        }

    }
    public class EtsyAttribute
    {
        public long attributeId { get; set; }
        public long? valueId { get; set; }
    }
}
