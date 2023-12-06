using FArmer_Central_Managament_app.Models;
using System.Data;
using System.Data.SqlClient;

namespace FArmer_Central_Managament_app.Data
{
    public class DbFarmer
    {
        private string Connectionstring;
        private IConfiguration _configuration;

        public DbFarmer(IConfiguration configuration)
        {
            _configuration = configuration;
            Connectionstring = _configuration.GetConnectionString("AzureDb");
        }
        public List<Farmer> AllFarmers()
        {
            
            List<Farmer> fList = new List<Farmer>();
            SqlConnection myConnection = new SqlConnection(Connectionstring);
            SqlDataAdapter myAdapter = new SqlDataAdapter("SELECT * FROM FARMER", myConnection);
            DataTable myTable = new DataTable();
            DataRow myRow = myTable.NewRow();

            string ID, Name, Surname, Email, Password;

            myConnection.Open();
            myAdapter.Fill(myTable);

            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    myRow = myTable.Rows[i];
                    fList.Add(new Farmer((string)myRow[0],
                        (string)myRow[1], (string)myRow[2], (string)myRow[3],
                        (string)myRow[4]));
                }
            }
            myConnection.Close();
            return fList;
        }
        public Farmer GetFarmer(string id)
        {
            Farmer farmer = new Farmer();
            SqlConnection myConnection = new SqlConnection(Connectionstring);
            SqlCommand cmdSelect =
                new SqlCommand($"SELECT * FROM FARMER WHERE FARMER_ID = '{id}'", myConnection);

            myConnection.Open();
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    farmer = new Farmer((string)reader[0], (string)reader[1], (string)reader[2],
                    (string)reader[3], (string)reader[4]);
                }
            }

            myConnection.Close();
            return farmer;
        }
        public void AddFarmers(Farmer farmer)
        {
            using (SqlConnection myConnection = new SqlConnection(Connectionstring))
            {
                SqlCommand cmdInsert = new SqlCommand($"INSERT INTO FARMER VALUES (@FarmerId, @Name, @Surname, @Email, @Password)", myConnection);

                // Set parameter values
                cmdInsert.Parameters.AddWithValue("@FarmerId", farmer.FarmerId);
                cmdInsert.Parameters.AddWithValue("@Name", farmer.Name);
                cmdInsert.Parameters.AddWithValue("@Surname", farmer.Surname);
                cmdInsert.Parameters.AddWithValue("@Email", farmer.Email);

                // Bcrypt hash the password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(farmer.Password);
                cmdInsert.Parameters.AddWithValue("@Password", hashedPassword);

                myConnection.Open();
                cmdInsert.ExecuteNonQuery();
            }
        }
        public void UpdateFarmer(string id, Farmer farmer)
        {
            using (SqlConnection myCon = new SqlConnection(Connectionstring))
            {
                SqlCommand cmdUpdate = new SqlCommand($"UPDATE FARMER SET FARMER_ID = @NewFarmerId, " +
                    $"NAME = @Name, " +
                    $"SURNAME = @Surname, " +
                    $"EMAIL = @Email, " +
                    $"PASSWORD = @Password " +
                    $"WHERE FARMER_ID = @OldFarmerId", myCon);

                // Set parameter values
                cmdUpdate.Parameters.AddWithValue("@NewFarmerId", farmer.FarmerId);
                cmdUpdate.Parameters.AddWithValue("@Name", farmer.Name);
                cmdUpdate.Parameters.AddWithValue("@Surname", farmer.Surname);
                cmdUpdate.Parameters.AddWithValue("@Email", farmer.Email);

                // Bcrypt hash the password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(farmer.Password);
                cmdUpdate.Parameters.AddWithValue("@Password", hashedPassword);

                cmdUpdate.Parameters.AddWithValue("@OldFarmerId", id);

                myCon.Open();
                cmdUpdate.ExecuteNonQuery();
            }
        }
        public void DeleteFarmers(string id)
        {
            using (SqlConnection myConn = new SqlConnection(Connectionstring))
            {
                SqlCommand cmdDelete =
                    new SqlCommand($"DELETE FROM FARMER WHERE FARMER_ID = '{id}'", myConn);
                myConn.Open();
                cmdDelete.ExecuteNonQuery();

            }
        }
        public List<Product> SearchProductsByName(string searchTerm)
        {
            List<Product> searchResults = new List<Product>();
            using (SqlConnection myConnection = new SqlConnection(Connectionstring))
            {
                string query = "SELECT * FROM FARMER_PRODUCTS WHERE PNAME LIKE '%' + @SearchTerm + '%' OR FARMER_ID LIKE '%' + @SearchTerm + '%'";
                SqlCommand cmdSelect = new SqlCommand(query, myConnection);
                cmdSelect.Parameters.AddWithValue("@SearchTerm", searchTerm);

                myConnection.Open();
                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string productID = (string)reader["PRODUCT_ID"];
                        string productName = (string)reader["PNAME"];
                        string productPrice = (string)reader["PPRICE"];
                        string quantity = (string)reader["PQUANTITY"];
                        string farmerID = (string)reader["FARMER_ID"];

                        Product product = new Product(productID, productName, productPrice, quantity, farmerID);
                        searchResults.Add(product);
                    }
                }
            }

            return searchResults;


        }
    }
}
