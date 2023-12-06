namespace FArmer_Central_Managament_app.Models
{
    public class Farmer
    {
        public Farmer(string farmerId, string name, string surname, string email, string password)
        {
            FarmerId = farmerId;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;

        }

        public string FarmerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Farmer()
        {

        }
    }
}
