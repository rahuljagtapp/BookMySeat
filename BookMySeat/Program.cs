using BookMySeat.Domain.Models;
using BookMySeat.Domain.Services;
using BookMySeat.Domain.Data;

namespace BookMySeat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Managers welcome to BookMySeat");



            var dataRepository = new DataRepository();
            var adminService = new AdminServicesForEmployee(dataRepository);
            var bookingService = new SeatService(dataRepository);




            //For Adding Employee By Admin
            var admin = dataRepository.Employees.FirstOrDefault(e => e.Role == Roles.Admin);
            var newEmployee = new Employee { Name = "Rahul Jagtap", Role = Roles.User };
            try
            {
                string result = adminService.AddEmployee(newEmployee, admin);
                Console.WriteLine(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operation failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }






            //For Deleting Employeee By Admin
            try
            {
                Guid employeeId = Guid.NewGuid();
                string result = adminService.DeleteEmployee(employeeId, admin);
                Console.WriteLine(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operation failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }










            //Seat Services

            //For Seat Adding
            Guid userId = dataRepository.Employees.FirstOrDefault(e => e.Role == Roles.User)?.EmployeeId ?? Guid.Empty;
            Guid resourceId = dataRepository.Resources.First().ResourceId;
            DateTime startDateTime = new DateTime(2024, 10, 30, 9, 0, 0);
            DateTime endDateTime = new DateTime(2024, 10, 30, 10, 0, 0);
            try
            {
                string bookingResult = bookingService.AddBooking(resourceId, userId, startDateTime, endDateTime);
                Console.WriteLine(bookingResult);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Add booking failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred while adding booking: {ex.Message}");
            }






            //For Seat UpDate
            List<BookingRecord> allBookings = bookingService.ViewAllBookings();
            Console.WriteLine("\nAll Bookings:");
            foreach (var booking in allBookings)
            {
                Console.WriteLine($"Booking ID: {booking.bookingId}, Employee ID: {booking.EmployeeId}, Resource ID: {booking.ResourceId}, Start Time: {booking.startDateTime}, End Time: {booking.endDateTime}");
            }
            var bookingToUpdate = allBookings.FirstOrDefault();
            if (bookingToUpdate != null)
            {
                DateTime newStartDateTime = new DateTime(2024, 10, 30, 11, 0, 0);
                DateTime newEndDateTime = new DateTime(2024, 10, 30, 12, 0, 0);

                try
                {
                    string updateResult = bookingService.UpdateBooking(bookingToUpdate.bookingId,userId,resourceId, newStartDateTime, newEndDateTime);
                    Console.WriteLine(updateResult);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Update booking failed: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while updating booking: {ex.Message}");
                }
            }




            //Method for Removing Booking
            try
            {
                Guid bookingIdToDelete = dataRepository.BookingRecords.First().bookingId;
                string deleteResult = bookingService.DeleteBooking(bookingIdToDelete, userId);
                Console.WriteLine(deleteResult);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Delete booking failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred while deleting booking: {ex.Message}");
            }




            //View Method For Particular User Id Booking

            try
            {
             
                List<BookingRecord> userBookings = bookingService.ViewBookingsByUser(userId);
                Console.WriteLine("Bookings for User:");
                foreach (var booking in userBookings)
                {
                    Console.WriteLine($"Booking ID: {booking.bookingId}, Resource ID: {booking.ResourceId}, Start Time: {booking.startDateTime}, End Time: {booking.endDateTime}");
                }

                
                Console.WriteLine("\nAll Bookings:");
                foreach (var booking in allBookings)
                {
                    Console.WriteLine($"Booking ID: {booking.bookingId}, Employee ID: {booking.EmployeeId}, Resource ID: {booking.ResourceId}, Start Time: {booking.startDateTime}, End Time: {booking.endDateTime}");
                }

                DateTime bookingDate = new DateTime(2024, 10, 30);
                List<BookingRecord> dateBookings = bookingService.ViewBookingsByDate(bookingDate);
                Console.WriteLine($"\nBookings for {bookingDate.ToShortDateString()}:");
                foreach (var booking in dateBookings)
                {
                    Console.WriteLine($"Booking ID: {booking.bookingId}, Employee ID: {booking.EmployeeId}, Resource ID: {booking.ResourceId}, Start Time: {booking.startDateTime}, End Time: {booking.endDateTime}");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operation failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }


            //View Method For all Booking
            List<BookingRecord> allBookingss = bookingService.ViewAllBookings();
            Console.WriteLine("\nAll Bookings:");
            foreach (var booking in allBookingss)
            {
                Console.WriteLine($"Booking ID: {booking.bookingId}, Employee ID: {booking.EmployeeId}, Resource ID: {booking.ResourceId}, Start Time: {booking.startDateTime}, End Time: {booking.endDateTime}");
            }




            //Admin can Only Add the Resource And Remove The Resource
            var admin1 = dataRepository.Employees.FirstOrDefault(e => e.Role == Roles.Admin);
            var adminServiceForResource = new AdminServiceForResources(dataRepository);
            if (admin1 == null)
            {
                Console.WriteLine("No admin found. Please add an admin user.");
                return;
            }
            try
            {
               
                string addResourceResult = adminServiceForResource.AddResource("Meeting Room", "Conference Room A",admin1.EmployeeId);
                Console.WriteLine(addResourceResult);

               
                Guid resourceIdToDelete = dataRepository.Resources.First().ResourceId; 
                string removeResourceResult = adminServiceForResource.RemoveResource(resourceIdToDelete,admin1.EmployeeId);
                Console.WriteLine(removeResourceResult);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operation failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }






            //Admin Services For Visitors

            var adminServiceForVisitor = new AdminServiceForVisitor(dataRepository);
            try
            {
               
                var newVisitor = new Visitor
                {
                    VisitorsId = 1,
                    VisitorsName = "Rahul J",
                    HostEmployee = "Abhishek Goyal"
                };

                string addVisitorResult = adminServiceForVisitor.AddVisitor(newVisitor, admin);
                Console.WriteLine(addVisitorResult);

       
                int visitorIdToDelete = 1;
                string deleteVisitorResult = adminServiceForVisitor.DeleteVisitor(visitorIdToDelete, admin);
                Console.WriteLine(deleteVisitorResult);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operation failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }



        }
    }
}
