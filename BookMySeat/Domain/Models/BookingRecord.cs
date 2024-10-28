namespace BookMySeat.Domain.Models
{
    public class BookingRecord
    {

        public Guid bookingId { get; set; }
        public Guid EmployeeId {get;set;}
        public Guid ResourceId { get; set; }
        public DateTime BookingDate { get; set; }

        public DateTime  startDateTime { get; set; }
        public DateTime endDateTime { get; set; }


    }
}
