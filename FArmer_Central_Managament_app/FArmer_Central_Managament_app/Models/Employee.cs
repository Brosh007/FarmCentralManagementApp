namespace FArmer_Central_Managament_app.Models
{
    public class Employee
    {
        public Employee(string employeeId, string empName, string empSurname, string empEmail, string empPassword)
        {
            EmployeeId = employeeId;
            EmpName = empName;
            EmpSurname = empSurname;
            EmpEmail = empEmail;
            EmpPassword = empPassword;
        }

        public string EmployeeId { get; set; }
        public string EmpName { get; set; }
        public string EmpSurname { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPassword { get; set; }
        public Employee()
        {

        }
        public void HashPassword()
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(EmpPassword);
            EmpPassword = hashedPassword;
        }
    }
    
}
