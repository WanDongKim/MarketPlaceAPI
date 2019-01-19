using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPlaceServiceAPI.Models;
using MarketPlaceServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IProductsService _productService;
        public DiscountController(IProductsService productsService)
        {
            _productService = productsService;
        }
        //GET: api/category
        [HttpGet()]
        [Route("alldiscounts")]
        [ProducesResponseType(typeof(IEnumerable<Discount>), 200)]
        public async Task<IEnumerable<Discount>> GetProductsAsync()
        {
            IEnumerable<Discount> discounts = await _productService.GetAllAsyncDiscount();

            return discounts;
        }
    }
}