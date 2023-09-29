using Microsoft.FeatureManagement;
using System.Data.SqlClient;
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
        public List<Product> GetProducts()
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
