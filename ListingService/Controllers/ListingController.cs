using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GarboSellsClasses.Models;
using ListingService.Database;
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

        public ListingController(IConfiguration configuration, EbayDatabaseContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        [Route("PostListing")]
        [HttpPost]
        public void PostListing([FromBody] Item item)
        {
            var category = context.categories
                .Where(c => c.garboSellsSubcategoryId == item.subcategoryId)
                .Include(c => c.categoryHasAspects)
                .ThenInclude(c => c.aspect)
                                .ToList()
                                .FirstOrDefault();
            var aspects = category.categoryHasAspects.Select(c => { return c.aspect;  }).ToList();

            var product = new Product();
            product.AddAspects(aspects, item.attributes);
            var x = product;
        }
    }

}
