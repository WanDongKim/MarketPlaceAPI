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
    public class MarketController : ControllerBase
    {
        private readonly IProductsService _productService;
        public MarketController(IProductsService productsService)
        {
            _productService = productsService;
        }
        //GET: api/category
        [HttpGet()]
        [Route("allmarkets")]
        [ProducesResponseType(typeof(IEnumerable<Market>), 200)]
        public async Task<IEnumerable<Market>> GetMarketsAsync()
        {
            IEnumerable<Market> markets = await _productService.GetAllAsyncMarket();

            return markets;
        }
    }
}