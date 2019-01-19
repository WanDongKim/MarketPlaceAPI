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
    public class CategoryController : ControllerBase
    {
        private readonly IProductsService _productService;
        public CategoryController(IProductsService productsService)
        {
            _productService = productsService;
        }
        //GET: api/category
        [HttpGet()]
        [Route("allcategory")]
        [ProducesResponseType(typeof(IEnumerable<Category>), 200)]
        public async Task<IEnumerable<Category>> GetProductsAsync()
        {
            IEnumerable<Category> categories = await _productService.GetAllAsyncCategory();

            return categories;
        }
    }
}