using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPlaceServiceAPI.Models;

namespace MarketPlaceServiceAPI.Services
{
    public interface IProductsService
    {
        Task AddAsync(Product product);
        Task AddRangeAsync(IEnumerable<Product> products);
        Task<Product> RemoveAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> FindAsync(int id);
        Task UpdateAsync(Product product);
        Task<IEnumerable<Category>> GetAllAsyncCategory();
        Task<IEnumerable<Market>> GetAllAsyncMarket();
        Task<IEnumerable<Discount>> GetAllAsyncDiscount();
    }
}
