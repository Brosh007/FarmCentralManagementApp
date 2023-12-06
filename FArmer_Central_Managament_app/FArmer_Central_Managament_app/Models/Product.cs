namespace FArmer_Central_Managament_app.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string Quantity { get; set; }
        public string FarmerID { get; set; }

        public Product()
        {

        }

        public Product(string productID, string productName, string productPrice, string quantity, string farmerID)
        {
            ProductID = productID;
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
            FarmerID = farmerID;
        }
    }
}
