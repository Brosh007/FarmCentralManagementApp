using FArmer_Central_Managament_app.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace FArmer_Central_Managament_app.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("AzureDb");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string employeeQuery = "SELECT * FROM EMPLOYEE WHERE EMP_EMAIL = @Email";
                    using (SqlCommand employeeCommand = new SqlCommand(employeeQuery, connection))
                    {
                        employeeCommand.Parameters.AddWithValue("@Email", model.Email);

                        using (SqlDataReader employeeReader = employeeCommand.ExecuteReader())
                        {
                            if (employeeReader.Read())
                            {
                                string hashedPassword = employeeReader.GetString(employeeReader.GetOrdinal("EMP_PASSWORD"));
                                if (BCrypt.Net.BCrypt.Verify(model.Password, hashedPassword))
                                {
                                    // Employee login successful
                                    // Perform any desired actions for employee login
                                    return RedirectToAction("Index", "Farmer");
                                }
                            }
                        }
                    }

                    string farmerQuery = "SELECT * FROM FARMER WHERE EMAIL = @Email";

                    using (SqlCommand farmerCommand = new SqlCommand(farmerQuery, connection))
                    {
                        farmerCommand.Parameters.AddWithValue("@Email", model.Email);

                        using (SqlDataReader farmerReader = farmerCommand.ExecuteReader())
                        {
                            if (farmerReader.Read())
                            {
                                string hashedPassword = farmerReader.GetString(farmerReader.GetOrdinal("PASSWORD"));
                                if (BCrypt.Net.BCrypt.Verify(model.Password, hashedPassword))
                                {
                                    // Retrieve the farmer's ID from the SqlDataReader
                                    string farmerID = farmerReader.GetString(farmerReader.GetOrdinal("FARMER_ID"));

                                    // Store the farmer's ID in the session
                                    HttpContext.Session.SetString("FARMER_ID", farmerID);
                                    // Farmer login successful
                                    // Perform any desired actions for farmer login
                                    return RedirectToAction("Index", "Product");
                                }
                            }
                        }
                    }
                }
            }

            // Invalid login credentials
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(model);
        }
        


    }
}


