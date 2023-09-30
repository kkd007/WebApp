using WebApp.Models;

namespace WebApp.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        public Task<bool> IsBeta();
    }
}