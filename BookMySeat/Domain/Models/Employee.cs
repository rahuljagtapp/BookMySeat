namespace BookMySeat.Domain.Models
{
    public class Employee
    {
         public Guid EmployeeId { get; set; }
         public  string Name { get; set; }

         public string Role { get; set; }

        public Employee()
        {
            EmployeeId = Guid.NewGuid();
        }

    }
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }

}
