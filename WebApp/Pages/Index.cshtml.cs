using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> Products;
        public void OnGet()
        {
            ProductService productService = new ProductService();
            Products = productService.GetProducts();
        }
    }
}
