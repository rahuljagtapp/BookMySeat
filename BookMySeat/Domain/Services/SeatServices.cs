using BookMySeat.Domain.Data;
using BookMySeat.Domain.Models;

namespace BookMySeat.Domain.Services
{
    public class SeatService
    {


        private readonly DataRepository _dataRepository;

        public SeatService(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }



        // Method to add a seat booking
        public string AddBooking(Guid resourceId, Guid employeeId, DateTime startDate, DateTime endDate)
        {
            
            var resource = _dataRepository.Resources.FirstOrDefault(r => r.ResourceId == resourceId);
            if (resource == null)
            {
                throw new InvalidOperationException("Resource not found.");
            }
            if (resource.IsBooked == true)
            {
                throw new InvalidOperationException("The resource is already booked.");
            }



            var employee = _dataRepository.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                throw new InvalidOperationException("Employee not found.");
            }
           


            if (startDate >= endDate)
            {
                throw new InvalidOperationException("Start date must be earlier than end date.");
            }

          
            var newBooking = new BookingRecord
            {
                bookingId = Guid.NewGuid(),
                ResourceId = resource.ResourceId,
                EmployeeId = employeeId,
                BookingDate = DateTime.Now,
                startDateTime = startDate,
                endDateTime = endDate
            };

            _dataRepository.BookingRecords.Add(newBooking);
            resource.IsBooked = true;

            return $"Booking for {resource.ResourceName} by employee {employee.Name} added successfully.";
        }












        // Method to update a booking
        public string UpdateBooking(Guid bookingId, Guid employeeId, Guid newResourceId, DateTime newStartDate, DateTime newEndDate)
        {
            var booking = _dataRepository.BookingRecords.FirstOrDefault(b => b.bookingId == bookingId);
            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found.");
            }

            var employee = _dataRepository.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                throw new InvalidOperationException("Employee not found.");
            }

           

            if (newStartDate >= newEndDate)
            {
                throw new InvalidOperationException("Start date must be earlier than end date.");
            }

            var newResource = _dataRepository.Resources.FirstOrDefault(r => r.ResourceId == newResourceId);
            if (newResource == null)
            {
                throw new InvalidOperationException("New resource not found.");
            }

            if ((bool)newResource.IsBooked)
            {
                throw new InvalidOperationException("The new resource is already booked.");
            }

            var conflictingBooking = _dataRepository.BookingRecords
                .Any(b => b.ResourceId == newResource.ResourceId &&
                           b.bookingId != booking.bookingId &&
                           ((newStartDate >= b.startDateTime && newStartDate < b.endDateTime) ||
                            (newEndDate > b.startDateTime && newEndDate <= b.endDateTime)));

            if (conflictingBooking)
            {
                throw new InvalidOperationException("The new resource is already booked for the selected time.");
            }

            booking.ResourceId = newResource.ResourceId;
            booking.startDateTime = newStartDate;
            booking.endDateTime = newEndDate;

            newResource.IsBooked = true;

            return $"Booking updated successfully to resource {newResource.ResourceName}, starting at {newStartDate} and ending at {newStartDate}.";
        }










        // Method to delete a booking

        public string DeleteBooking(Guid bookingId, Guid employeeId)
        {
            var booking = _dataRepository.BookingRecords.FirstOrDefault(b => b.bookingId == bookingId);
            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found.");
            }

            var employee = _dataRepository.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                throw new InvalidOperationException("Employee not found.");
            }

            var resource = _dataRepository.Resources.FirstOrDefault(r => r.ResourceId == booking.ResourceId);
            if (resource != null)
            {
                resource.IsBooked = false;
            }

            _dataRepository.BookingRecords.Remove(booking);
            return $"Booking with ID {bookingId} deleted successfully.";
        }





        //Method:View Method For Booking by Particular user id
        public List<BookingRecord> ViewBookingsByUser(Guid employeeId)
        {
            var bookings = _dataRepository.BookingRecords
                .Where(b => b.EmployeeId == employeeId)
                .ToList();

            if (!bookings.Any())
            {
                throw new InvalidOperationException("No bookings found for this user.");
            }

            return bookings;
        }





        //Method :View Method For All Booking
        public List<BookingRecord> ViewAllBookings()
        {
            if (!_dataRepository.BookingRecords.Any())
            {
                throw new InvalidOperationException("No bookings available.");
            }

            return _dataRepository.BookingRecords;
        }







        //Method :view Method For Booking For Particular Date
        public List<BookingRecord> ViewBookingsByDate(DateTime date)
        {
            var bookings = _dataRepository.BookingRecords
                .Where(b => b.startDateTime.Date == date.Date || b.endDateTime.Date == date.Date)
                .ToList();

            if (!bookings.Any())
            {
                throw new InvalidOperationException("No bookings found for this date.");
            }

            return bookings;
        }









    }
}
