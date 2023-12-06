using FArmer_Central_Managament_app.Models;
using System.Data;
using System.Data.SqlClient;

namespace FArmer_Central_Managament_app.Data
{
    public class DbProduct
    {
        private string connectionString;

        private IConfiguration _config;
        public DbProduct(IConfiguration configuration)
        {
            _config = configuration;
            connectionString = _config.GetConnectionString("AzureDb");
        }

        public List<Product> AllProducts(string farmerID)
        {


            List<Product> productList = new List<Product>();
             
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM FARMER_PRODUCTS WHERE FARMER_ID = @FarmerID";
                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    command.Parameters.AddWithValue("@FarmerID", farmerID);
                    myConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productList.Add(new Product
                            {
                                ProductID = reader.GetString(reader.GetOrdinal("PRODUCT_ID")),
                                ProductName = reader.GetString(reader.GetOrdinal("PNAME")),
                                ProductPrice = reader.GetString(reader.GetOrdinal("PPRICE")),
                                Quantity = reader.GetString(reader.GetOrdinal("PQUANTITY")),
                                FarmerID = reader.GetString(reader.GetOrdinal("FARMER_ID"))
                            });
                        }
                    }
                }
            }
            return productList;
        }
    
        public Product GetProductClass(string id)
        {
            Product product = new Product();
            SqlConnection myConnection = new SqlConnection(connectionString);
            SqlCommand cmdSelect =
                new SqlCommand($"SELECT * FROM FARMER_PRODUCTS WHERE PRODUCT_ID = '{id}'", myConnection);

            myConnection.Open();
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    product = new Product((string)reader[0], (string)reader[1], (string)reader[2],
                    (string)reader[3], (string)reader[4]);
                }
            }

            myConnection.Close();
            return product;
        }
        public void AddProducts(Product product)
        {
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmdInsert =
                    new SqlCommand($"INSERT INTO FARMER_PRODUCTS VALUES" +
                    $"('{product.ProductID}'," +
                    $"'{product.ProductName}'," +
                    $"'{product.ProductPrice}'," +
                    $"'{product.Quantity}'," +
                    $"'{product.FarmerID}')", myConnection);

                myConnection.Open();
                cmdInsert.ExecuteNonQuery();
            }
        }
        public void UpdateProduct(string id, Product product)
        {
            using (SqlConnection myCon = new SqlConnection(connectionString))
            {
                SqlCommand cmdUpdate = new SqlCommand($"UPDATE FARMER_PRODUCTS SET PRODUCT_ID ='{product.ProductID}'," +
                    $"PNAME = '{product.ProductName}'," +
                    $"PPRICE = '{product.ProductPrice}'," +
                    $"PQUANTITY = '{product.Quantity}'," +
                    $"FARMER_ID = '{product.FarmerID}'" +
                    $" WHERE PRODUCT_ID= '{id}'", myCon);

                myCon.Open();
                cmdUpdate.ExecuteNonQuery();
            }
        }
        public void DeleteProduct(string id)
        {
            using (SqlConnection myConn = new SqlConnection(connectionString))
            {
                SqlCommand cmdDelete =
                    new SqlCommand($"DELETE FROM FARMER_PRODUCTS WHERE PRODUCT_ID = '{id}'", myConn);
                myConn.Open();
                cmdDelete.ExecuteNonQuery();

            }
        }
    }
}
