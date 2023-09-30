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
        public bool IsBeta = false;

        public IndexModel(IProductService productService)
        {
            Products = new List<Product>();
            _productService = productService;
        }
        public void OnGet()
        {
            IsBeta  = _productService.IsBeta().Result;
            Console.WriteLine("Beta Feature : " + IsBeta);
            Products = _productService.GetProducts().GetAwaiter().GetResult();
        }
       
    }
}
