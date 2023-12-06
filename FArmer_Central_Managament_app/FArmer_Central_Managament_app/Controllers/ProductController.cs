using FArmer_Central_Managament_app.Data;
using FArmer_Central_Managament_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FArmer_Central_Managament_app.Controllers
{
    public class ProductController : Controller
    {
        DbProduct dBProductHelper;
        public ProductController(IConfiguration config)
        {
            dBProductHelper = new DbProduct(config);
        }
        // GET: ProductController
        public ActionResult Index()
        {
            string farmerID = HttpContext.Session.GetString("FARMER_ID");

            if (farmerID != null)
            {
                List<Product> productList = dBProductHelper.AllProducts(farmerID);
                return View(productList);
            }
            else
            {
                // Handle the case when the farmerID is null, such as redirecting to a login page
                // or displaying an error message.
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: ProductController/Details/5
        public ActionResult Details(string id)
        {
            Product product = dBProductHelper.GetProductClass(id);
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string productID = collection["txtProductID"];
                string productName = collection["txtProductName"];
                string productPrice = collection["txtProductPrice"];
                string productQuantity = collection["txtQuantity"];
                string Farmer_ID = collection["txtFarmerID"];

                Product product = new Product(productID, productName, productPrice, productQuantity, Farmer_ID);
                dBProductHelper.AddProducts(product);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(string id)
        {
            Product product = dBProductHelper.GetProductClass(id);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                
                string name = collection["txtProName"];
                string price = collection["txtProPrice"];
                string quantity = collection["txtProQuan"];
                string Fid = collection["txtFID"];
                Product product = new Product(id, name, price, quantity, Fid);

                dBProductHelper.UpdateProduct(id, product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(string id)
        {
            Product product = dBProductHelper.GetProductClass(id);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                dBProductHelper.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
