using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        public List<Product> Products;

        public IndexModel(IProductService productService)
        {
            Products = new List<Product>();
            _productService = productService;
        }
        public void OnGet()
        {
            Products = _productService.GetProducts();
        }
    }
}
