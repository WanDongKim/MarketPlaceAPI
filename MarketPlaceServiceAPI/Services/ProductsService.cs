using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using MarketPlaceServiceAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceServiceAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly MarketPlaceServiceAPIContext _context;
        private readonly ConcurrentDictionary<int, Product> _products = new ConcurrentDictionary<int, Product>();
        public ProductsService(MarketPlaceServiceAPIContext context)
        {
            _context = context;
            foreach(var a in context.Product.Include(c => c.Category)
                .Include(c => c.Discount)
                .Include(c => c.Market))
            {
                
                _products.TryAdd(a.ProductId, a);
            }
        }
        public Task AddAsync(Product product)
        {
            //_products[product.ProductId] = product;
            _context.Add(product);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                _context.AddAsync(product);
                //_products[product.ProductId] = product;
            }
            return Task.CompletedTask;
        }
        public Task<Product> RemoveAsync(int id)
        {
            var product = _context.Product.Find(id);
            //_products.TryRemove(id, out Product removed);
            _context.Product.Remove(product);
            _context.SaveChanges();

            return Task.FromResult(product);
        }
        public Task<IEnumerable<Product>> GetAllAsync()
        {
            IEnumerable<Product> product = _context.Product
                .Include(c => c.Category)
                .Include(c => c.Discount)
                .Include(c => c.Market).AsEnumerable();
            return Task.FromResult(product);
            //return Task.FromResult<IEnumerable<Product>>(_products.Values);
        } 

        public Task<Product> FindAsync(int id)
        {
            var product = _context.Product
                .Include(c => c.Category)
                .Include(c => c.Discount)
                .Include(c => c.Market)
                .FirstOrDefault(m => m.ProductId == id);
            //Product product = _context.Product.FindAsync(id);
            //_products.TryGetValue(id, out Product product);

            return Task.FromResult(product);
        }

        public Task UpdateAsync(Product product)
        {
            var beforeUpdate = _context.Product.Single(s => s.ProductId == product.ProductId);
            beforeUpdate.Name = product.Name;
            beforeUpdate.CategoryId = product.CategoryId;
            beforeUpdate.DiscountId = product.DiscountId;
            beforeUpdate.MarketId = product.MarketId;
            beforeUpdate.Price = product.Price;

            _context.Product.Update(beforeUpdate);
            _context.SaveChanges();
            //_products[product.ProductId] = product;
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Category>> GetAllAsyncCategory()
        {
            IEnumerable<Category> category = _context.Category.AsEnumerable();

            return Task.FromResult(category);
        }
        public Task<IEnumerable<Market>> GetAllAsyncMarket()
        {
            IEnumerable<Market> markets = _context.Market.AsEnumerable();

            return Task.FromResult(markets);
        }
        public Task<IEnumerable<Discount>> GetAllAsyncDiscount()
        {
            IEnumerable<Discount> discounts = _context.Discount.AsEnumerable();

            return Task.FromResult(discounts);
        }
        
    }
}
