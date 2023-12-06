using FArmer_Central_Managament_app.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace FArmer_Central_Managament_app.Controllers
{
    public class EmpRegController : Controller
    {
        private readonly IConfiguration _configuration;

        public EmpRegController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public ActionResult Register()
        {
            Employee employee = new Employee();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Register(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.HashPassword(); // Hash the password using bcrypt

                // Insert the employee into the database
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("AzureDb")))
                {
                    string query = "INSERT INTO EMPLOYEE (EMPLOYEE_ID, EMP_NAME, EMP_SURNAME, EMP_EMAIL, EMP_PASSWORD) " +
                                   "VALUES (@EmployeeId, @EmpName, @EmpSurname, @EmpEmail, @EmpPassword)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    command.Parameters.AddWithValue("@EmpName", employee.EmpName);
                    command.Parameters.AddWithValue("@EmpSurname", employee.EmpSurname);
                    command.Parameters.AddWithValue("@EmpEmail", employee.EmpEmail);
                    command.Parameters.AddWithValue("@EmpPassword", employee.EmpPassword);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                // Registration successful, redirect to login
                return RedirectToAction("Login", "Account");
            }

            // If the model state is invalid, return to the registration form
            return View(employee);
        }

        
    }
}
