using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GarboSellsClasses.Models;
using ListingService.Database;
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

        public ListingController(IConfiguration configuration, EbayDatabaseContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        [Route("PostListing")]
        [HttpGet]
        public void PostListing(Item item)
        {
            var categories = context.categories
                .Include(c => c.categoryHasAspects)
                .ThenInclude(c => c.aspect)
                                .ToList();
            var x = categories;
        }
    }

}
