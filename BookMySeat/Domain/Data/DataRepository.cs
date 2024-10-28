using BookMySeat.Domain.Models;
namespace BookMySeat.Domain.Data
{
    public class DataRepository
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Resource> Resources { get; set; } = new List<Resource>();
        public List<BookingRecord> BookingRecords { get; set; } = new List<BookingRecord>();

        public List<Visitor> Visitors { get; set; } = new List<Visitor>();





        public DataRepository()
        {
            InitializeEmployees();
            InitializeResources();
            InitializeBookingRecords();
            InitializeVisitors();

        }

        private void InitializeEmployees()
        {
            Employees.Add(new Employee { Name = "Aarav Sharma", Role = Roles.Admin });
            Employees.Add(new Employee { Name = "Vihaan Patel", Role = Roles.User });
            Employees.Add(new Employee { Name = "Aditya Singh", Role = Roles.User });
            Employees.Add(new Employee { Name = "Ananya Gupta", Role = Roles.Admin });
            Employees.Add(new Employee { Name = "Priya Mehta", Role = Roles.User });
            Employees.Add(new Employee { Name = "Rahul Verma", Role = Roles.User });
            Employees.Add(new Employee { Name = "Riya Desai", Role = Roles.Admin });
            Employees.Add(new Employee { Name = "Karan Bhatia", Role = Roles.User });
            Employees.Add(new Employee { Name = "Sneha Joshi", Role = Roles.User });
            Employees.Add(new Employee { Name = "Arjun Iyer", Role = Roles.Admin });

        }




        private void InitializeResources()
        {
            Resources.Add(new Resource("Desk", "Desk A"));
            Resources.Add(new Resource("Desk", "Desk B"));
            Resources.Add(new Resource("Desk", "Desk C"));
            Resources.Add(new Resource("Meeting Room", "Meeting Room 1"));
            Resources.Add(new Resource("Meeting Room", "Meeting Room 2"));
        }



        private void InitializeBookingRecords()
        {
            BookingRecords.Add(new BookingRecord
            {
                bookingId = Guid.NewGuid(),
                EmployeeId = Employees.First(e => e.Role == Roles.User).EmployeeId,
                ResourceId = Resources.First().ResourceId,
                BookingDate = new DateTime(2024, 8, 24),
                startDateTime = new DateTime(2024, 8, 24, 9, 0, 0),
                endDateTime = new DateTime(2024, 8, 24, 10, 0, 0)
            });
            BookingRecords.Add(new BookingRecord
            {
                bookingId = Guid.NewGuid(),
                EmployeeId = Employees.First(e => e.Role == Roles.User).EmployeeId,
                ResourceId = Resources.Last().ResourceId,
                BookingDate = new DateTime(2024, 8, 24),
                startDateTime = new DateTime(2024, 8, 24, 11, 0, 0),
                endDateTime = new DateTime(2024, 8, 24, 12, 0, 0)
            });
        }


        private void InitializeVisitors()
        {
            Visitors.Add(new Visitor { VisitorsId = 1, VisitorsName = "Rahul J", HostEmployee = "Abhishek Goyal" });
            Visitors.Add(new Visitor { VisitorsId = 2, VisitorsName = "Dhurevee K", HostEmployee = "Abhishek Goyal" });
            Visitors.Add(new Visitor { VisitorsId = 3, VisitorsName = "Prathmesh P", HostEmployee = "Abhishek Goyal" });
        }


    }
}
