using BookMySeat.Domain.Data;
using BookMySeat.Domain.Models;

namespace BookMySeat.Domain.Services
{
    public class AdminServicesForEmployee
    {
        private readonly DataRepository _dataRepository;

        public AdminServicesForEmployee(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }



        //Add Employee Method For Admin Only

        public string AddEmployee(Employee newEmployee, Employee admin)
        {
            if (admin.Role != Roles.Admin)
            {
                throw new UnauthorizedAccessException("Only admins can add employees.");
            }

            if (_dataRepository.Employees.Any(e => e.EmployeeId == newEmployee.EmployeeId))
            {
                return "An employee with this ID already exists.";
            }

            _dataRepository.Employees.Add(newEmployee);
            return $"Employee {newEmployee.Name} added successfully.";
        }





        //Delete Employee Method For Admin Only 

        public string DeleteEmployee(Guid employeeId, Employee admin)
        {
            if (admin.Role != Roles.Admin)
            {
                throw new UnauthorizedAccessException("Only admins can delete employees.");
            }

            var employee = _dataRepository.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                throw new InvalidOperationException("Employee not found.");
            }

            _dataRepository.Employees.Remove(employee);
            return $"Employee {employee.Name} deleted successfully.";
        }

        



















    }
}
