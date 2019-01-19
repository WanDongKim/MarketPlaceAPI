using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPlaceServiceAPI.Models;
using MarketPlaceServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceServiceAPI.Controllers
{   [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;
        public ProductsController(IProductsService productsService)
        {
            _productService = productsService;
        }

        //GET: api/products
        [HttpGet()]
        [Route("allproducts")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            IEnumerable<Product> products = await _productService.GetAllAsync();

            return products;
        }

        //GET api/products/id
        [HttpGet("{id}", Name = nameof(GetProductByIdAsync))]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            Product product = await _productService.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(product);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostProductAsync([FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            await _productService.AddAsync(product);
            return CreatedAtRoute(nameof(GetProductByIdAsync), new { id = product.ProductId }, product);
        }

        //PUT api/products/id
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutProductAsync(int id, [FromBody]Product product)
        {
            if (product == null || id != product.ProductId)
            {
                return BadRequest();
            }
            if (await _productService.FindAsync(id) == null)
            {
                return NotFound();
            }
            await _productService.UpdateAsync(product);
            return new NoContentResult();
        }

        //DELETE api/products/id
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id) => await _productService.RemoveAsync(id);
    }
}