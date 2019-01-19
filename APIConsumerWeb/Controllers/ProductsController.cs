using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APIConsumerWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace APIConsumerWeb.Controllers
{
    public class ProductsController : Controller
    {
        public static HttpClient client = new HttpClient();
        private static IEnumerable<Category> categories;
        private static IEnumerable<Market> markets;
        private static IEnumerable<Discount> discounts;


        public ProductsController()
        {
            if (client.BaseAddress == null)
            {
                //initData();
                client.BaseAddress = new Uri("http://dkim125-eval-test.apigee.net/apimarketservice/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "4ZDWsju5RdiHIIJTp5NU6QFIyfnNA4rM");

            }
        }

        public async Task<IActionResult> Index()
        {
            await initDataFromAPIAsync();

            string json;
            HttpResponseMessage response;
            IEnumerable<Product> products;
            try
            {

                response = await client.GetAsync("api/products/allproducts");

                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                    return View(products);
                }
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
            return View();
        }
        public async Task<ActionResult> Details(int id)
        {
            Product product;
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("api/products/" + id);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<Product>();
                    return View(product);
                }
            }
            catch(Exception e)
            {
                return View(e.Message);
            }
            return View();
        }
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(categories, "CategoryId", "Name");
            ViewData["Discount"] = new SelectList(discounts, "DiscountId", "OfferAsPercent");
            ViewData["Market"] = new SelectList(markets, "MarketId", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId, Name, Price, CategoryId, MarketId, DiscountId")]Product product, IFormFile file)
        {
            HttpResponseMessage response;
            ViewData["Category"] = new SelectList(categories, "CategoryId", "Name");
            ViewData["Discount"] = new SelectList(discounts, "DiscountId", "OfferAsPercent");
            ViewData["Market"] = new SelectList(markets, "MarketId", "Name");
            var stream = file.OpenReadStream();
            string filename = product.CategoryId + "-" + product.DiscountId + "-"+ product.MarketId + "-" + product.Name + ".jpg";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + "images/products/");

            using (var fileStream = new FileStream(Path.Combine(filePath, filename), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            product.PictureId = filename;
                response = await client.PostAsJsonAsync("api/products", product);
            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Category"] = new SelectList(categories, "CategoryId", "Name");
            ViewData["Discount"] = new SelectList(discounts, "DiscountId", "OfferAsPercent");
            ViewData["Market"] = new SelectList(markets, "MarketId", "Name");
            if (id == null)
            {
                return NotFound();
            }
            Product product;
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("api/products" + id);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<Product>();
                    return View(product);
                }
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId, Name, Price, CategoryId, MarketId, DiscountId")]Product product)
        {
            ViewData["Category"] = new SelectList(categories, "CategoryId", "Name");
            ViewData["Discount"] = new SelectList(discounts, "DiscountId", "OfferAsPercent");
            ViewData["Market"] = new SelectList(markets, "MarketId", "Name");
            HttpResponseMessage response;
            product.ProductId = id;
            response = await client.PutAsJsonAsync("api/products/" + id, product);
            return RedirectToAction("Details",new { id });
        }
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Product product;
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("api/products/" + id);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<Product>();
                    return View(product);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product product;
            HttpResponseMessage response;
            try
            {
                response = await client.DeleteAsync("api/products/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                return NotFound();
            }
            return View();
        }
        private async Task initDataFromAPIAsync()
        {
            string json;
            HttpResponseMessage response;
                response = await client.GetAsync("api/category/allcategory");

                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(json);
                }

            response = await client.GetAsync("api/discount/alldiscounts");

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                discounts = JsonConvert.DeserializeObject<IEnumerable<Discount>>(json);
            }
            response = await client.GetAsync("api/market/allmarkets");

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                markets = JsonConvert.DeserializeObject<IEnumerable<Market>>(json);
            }

        }
    }
}