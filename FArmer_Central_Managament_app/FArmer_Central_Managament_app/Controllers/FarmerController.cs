using FArmer_Central_Managament_app.Data;
using FArmer_Central_Managament_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FArmer_Central_Managament_app.Controllers
{
    public class FarmerController : Controller
    {
        DbFarmer HelperFarmer;
        public FarmerController(IConfiguration config)
        {
            HelperFarmer = new DbFarmer(config);
        }
        // GET: FarmerController
        public ActionResult Index()
        {
            List<Farmer> farmerList = HelperFarmer.AllFarmers();
            return View(farmerList);
        }

        // GET: FarmerController/Details/5
        public ActionResult Details(string id)
        {
            Farmer farmer = HelperFarmer.GetFarmer(id);
            return View(farmer);
        }

        // GET: FarmerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FarmerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string FarmerID = collection["txtFarmerId"];
                string FarmerName = collection["txtName"];
                string FarmerSurName = collection["txtSurName"];
                string FarmerEmail = collection["txtEmail"];
                string FarmerPassword = collection["txtPassword"];

                

                Farmer farmer = new Farmer(FarmerID, FarmerName, FarmerSurName, FarmerEmail, FarmerPassword);
                HelperFarmer.AddFarmers(farmer);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/Edit/5
        public ActionResult Edit(string id)
        {
            Farmer farmer = HelperFarmer.GetFarmer(id);
            return View(farmer);
        }

        // POST: FarmerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {

                string FarmerName = collection["txtFName"];
                string FarmerSurName = collection["txtfSName"];
                string FarmerEmail = collection["txtfEmail"];
                string FarmerPassword = collection["txtfPassword"];
                Farmer farmer = new Farmer(id, FarmerName, FarmerSurName, FarmerEmail, FarmerPassword);

                HelperFarmer.UpdateFarmer(id, farmer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/Delete/5
        public ActionResult Delete(string id)
        {
            Farmer farmer = HelperFarmer.GetFarmer(id);
            return View(farmer);
        }
        
        // POST: FarmerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                HelperFarmer.DeleteFarmers(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: FarmerController/SearchProductsByName
        public ActionResult SearchProductsByName()
        {
            return View();
        }

        // POST: FarmerController/SearchProductsByName
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchProductsByName(string searchTerm)
        {
            List<Product> searchResults = HelperFarmer.SearchProductsByName(searchTerm);
            return View(searchResults);
        }
    }
}
