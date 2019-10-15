using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GarboSellsClasses.Models;
using ListingService.Database;
using ListingService.Managers;
using ListingService.Models.EbayClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ListingService.Controllers
{
    [Route("api/[controller]")]
    public class ListingController : Controller
    {
        private IConfiguration configuration;
        private readonly EbayDatabaseContext context;
        private ListingManager listingManager;

        public ListingController(IConfiguration configuration, EbayDatabaseContext context)
        {
            this.configuration = configuration;
            this.context = context;
            this.listingManager = new ListingManager(context);
        }

        [Route("PostListing")]
        [HttpPost]
        public void PostListing([FromBody] Item item)
        {
            var ebayInventoryItem = listingManager.MapItemToEbayProduct(item);
            var x = ebayInventoryItem;
        }
    }

}
