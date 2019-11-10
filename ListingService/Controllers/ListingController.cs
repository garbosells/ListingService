using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GarboSellsClasses.Models;
using ListingService.EbayDatabase;
using ListingService.EtsyDatabase;
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
        private readonly EbayDatabaseContext ebayDbContext;
        private readonly EtsyDatabaseContext etsyDbContext;
        private EbayListingManager ebayListingManager;
        private EtsyListingManager etsyListingManager;

        public ListingController(IConfiguration configuration, EbayDatabaseContext ebayDbContext, EtsyDatabaseContext etsyDbContext)
        {
            this.configuration = configuration;
            this.ebayDbContext = ebayDbContext;
            this.etsyDbContext = etsyDbContext;
            this.ebayListingManager = new EbayListingManager(ebayDbContext);
            this.etsyListingManager = new EtsyListingManager(etsyDbContext);
        }

        [Route("PostListing")]
        [HttpPost]
        public PostGarboSellsListingResponse PostListing([FromBody] PostGarboSellsListingRequest request)
        {
            var response = new PostGarboSellsListingResponse();
            try
            {
                Task<PostListingResponse> ebayTask = null;
                Task<PostListingResponse> etsyTask = null;
                if (request.postToEbay || request.postToEtsy)
                {
                    var tasks = new Task[] { };
                    if (request.postToEbay)
                    {
                        ebayTask = PostListingToEbay(request.listing.inventoryItem);
                        tasks.Append(ebayTask);
                    }
                        
                    if (request.postToEtsy)
                    {
                        etsyTask = PostListingToEtsy(request.listing.inventoryItem);
                        tasks.Append(etsyTask);
                    }

                    Task.WaitAll(tasks);
                    
                    if (ebayTask != null && ebayTask.IsCompleted)
                    {
                        response.PostEbayListingResponse = new PostListingResponse
                        {
                            IsSuccess = ebayTask.IsCompletedSuccessfully && ebayTask.Result.IsSuccess,
                            ListingId = ebayTask.IsCompletedSuccessfully ? ebayTask.Result.ListingId : null,
                            ErrorMessage = ebayTask.IsCompletedSuccessfully ? null : ebayTask.Result.ErrorMessage
                        };
                    }
                    if (etsyTask != null && etsyTask.IsCompleted)
                    {
                        response.PostEbayListingResponse = new PostListingResponse
                        {
                            IsSuccess = etsyTask.IsCompletedSuccessfully && etsyTask.Result.IsSuccess,
                            ListingId = etsyTask.IsCompletedSuccessfully ? etsyTask.Result.ListingId : null,
                            ErrorMessage = etsyTask.IsCompletedSuccessfully ? null : etsyTask.Result.ErrorMessage
                        };
                    }
                    response.IsSuccess = true;
                    return response;

                }
                else
                {
                    throw new Exception("Problem with request: no marketplace selected");
                }

            }
            catch (Exception ex)
            {
                return new PostGarboSellsListingResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private async Task<PostListingResponse> PostListingToEtsy(Item item)
        {
            //create a draft listing object
            //then generate post request that will be required to add attributes
            return null;
        }

        private async Task<PostListingResponse> PostListingToEbay(Item item)
        {
            try
            {
                //map to Ebay inventory item
                var ebayInventoryItemWrapper = ebayListingManager.MapItemToEbayInventoryItem(item);

                var ebayInventoryItem = ebayInventoryItemWrapper.ebayInventoryItem;
                var categoryId = ebayInventoryItemWrapper.ebayCategoryId;

                var defaults = ebayDbContext.defaults.First();

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
                    price = item.price
                };

                var uri = "https://ebayservice-test.azurewebsites.net/api/Listing/PostListing";
                //var uri = "https://localhost:5001/api/Listing/PostListing";
                var client = new HttpClient();
                client.Timeout = new TimeSpan(0, 5, 0);
                var httpResponse = client.PostAsJsonAsync(uri, postListingRequest);
                if(httpResponse.Result.IsSuccessStatusCode)
                {
                    var response = await httpResponse.Result.Content.ReadAsStringAsync();
                    return new PostListingResponse
                    {
                        IsSuccess = true,
                        ListingId = response
                    };
                }
                throw new Exception("Error occurred while posting to Ebay: " + httpResponse.Result.ReasonPhrase);

            }
            catch (Exception ex)
            {
                return new PostListingResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

    }

}
