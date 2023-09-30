using Microsoft.FeatureManagement;
using System.Data.SqlClient;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class ProductService : IProductService
    {
        private static string db_Source = "appserversky.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "Welcome1@";
        private static string db_database = "appdb";
        private readonly IConfiguration _configuration;
       
        private readonly IFeatureManager _featureManager;

        public ProductService(IConfiguration configuration, IFeatureManager featureManager )
        {
            _configuration = configuration;
            _featureManager = featureManager;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration["SQLConnection"]);
        }

        public async Task<bool> IsBeta()
        {
            if (await _featureManager.IsEnabledAsync("beta"))
            {
                return true;
            }
            else { return false; }
        }
        public async Task <List<Product>> GetProducts()
        {
            //use the correct function url that is created on the same Azure App Service plan
            string FunctionUrl = "https://func-httptrigger-azureskys.azurewebsites.net/api/GetProducts?code=_xPg76NUO1bK1ExVxTWFiAskl8UHE8igE-H1tIGAfpKyAzFuQ3sozw==";

            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(FunctionUrl);
                HttpResponseMessage  httpResponse = await client.GetAsync(FunctionUrl);

                string content = await httpResponse.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Product>>(content);
            }
        }
        public List<Product> GetProducts2()
        {
            SqlConnection conn = GetConnection(); ;
            List<Product> products = new List<Product>();
            string statement = "SELECT ProductID, ProductName, Quantity from Products";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;
        }
    }
}
