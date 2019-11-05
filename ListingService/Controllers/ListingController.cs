using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GarboSellsClasses.Models;
using ListingService.EbayDatabase;
using ListingService.Managers;
using ListingService.Models.API;
using ListingService.Models.EbayClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ListingService.Controllers
{
    [Route("api/[controller]")]
    public class ListingController : Controller
    {
        private IConfiguration configuration;
        private readonly EbayDatabaseContext context;
        private EbayListingManager listingManager;

        public ListingController(IConfiguration configuration, EbayDatabaseContext context)
        {
            this.configuration = configuration;
            this.context = context;
            this.listingManager = new EbayListingManager(context);
        }

        [Route("PostListing")]
        [HttpPost]
        public async Task<string> PostListingAsync([FromBody] Item item)
        {
            try
            {
                //map to Ebay inventory item
                var ebayInventoryItemWrapper = listingManager.MapItemToEbayInventoryItem(item);

                var ebayInventoryItem = ebayInventoryItemWrapper.ebayInventoryItem;
                var categoryId = ebayInventoryItemWrapper.ebayCategoryId;

                var defaults = context.defaults.First();

                var paymentPolicyId = defaults.paymentPolicyId;
                var fulfillmentPolicyId = defaults.fulfillmentPolicyId;
                var returnPolicyId = defaults.returnPolicyId;
                var merchantLocationKey = defaults.merchantLocationKey;

                var postListingRequest = new PostEbayListingRequest
                {
                    paymentPolicyId = paymentPolicyId,
                    fulfillmentPolicyId = fulfillmentPolicyId,
                    returnPolicyId = returnPolicyId,
                    categoryId = categoryId.ToString(),
                    merchantLocationKey = merchantLocationKey,
                    inventoryItem = ebayInventoryItem,
                    price = "5.00"
                };

                var uri = "https://localhost:5001/api/Listing/PostListing";
                var client = new HttpClient();
                client.Timeout = new TimeSpan(0, 5, 0);
                var httpResponse = client.PostAsJsonAsync(uri, postListingRequest);
                var response = await httpResponse.Result.Content.ReadAsStringAsync();
                return response;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private async Task<string> PostListingToEbay([FromBody] Item item)
        {
            try
            {
                //map to Ebay inventory item
                var ebayInventoryItemWrapper = listingManager.MapItemToEbayInventoryItem(item);

                var ebayInventoryItem = ebayInventoryItemWrapper.ebayInventoryItem;
                var categoryId = ebayInventoryItemWrapper.ebayCategoryId;

                var defaults = context.defaults.First();

                var paymentPolicyId = defaults.paymentPolicyId;
                var fulfillmentPolicyId = defaults.fulfillmentPolicyId;
                var returnPolicyId = defaults.returnPolicyId;
                var merchantLocationKey = defaults.merchantLocationKey;

                var postListingRequest = new PostEbayListingRequest
                {
                    paymentPolicyId = paymentPolicyId,
                    fulfillmentPolicyId = fulfillmentPolicyId,
                    returnPolicyId = returnPolicyId,
                    categoryId = categoryId.ToString(),
                    merchantLocationKey = merchantLocationKey,
                    inventoryItem = ebayInventoryItem,
                    price = "5.00"
                };

                var uri = "https://localhost:5001/api/Listing/PostListing";
                var client = new HttpClient();
                client.Timeout = new TimeSpan(0, 5, 0);
                var httpResponse = client.PostAsJsonAsync(uri, postListingRequest);
                var response = await httpResponse.Result.Content.ReadAsStringAsync();
                return response;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }

}
