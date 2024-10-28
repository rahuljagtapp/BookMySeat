using BookMySeat.Domain.Data;
using BookMySeat.Domain.Models;

namespace BookMySeat.Domain.Services
{
    public class AdminServiceForVisitor
    {


        private readonly DataRepository _dataRepository;

        public AdminServiceForVisitor(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }






        // Add Visitor Method for Admin Only
        public string AddVisitor(Visitor newVisitor, Employee admin)
        {
            if (admin.Role != Roles.Admin)
            {
                throw new UnauthorizedAccessException("Only admins can add visitors.");
            }

            if (_dataRepository.Visitors.Any(v => v.VisitorsId == newVisitor.VisitorsId))
            {
                return "A visitor with this ID already exists.";
            }

            _dataRepository.Visitors.Add(newVisitor);
            return $"Visitor {newVisitor.VisitorsName} added successfully.";
        }







        // Delete Visitor Method for Admin Only
        public string DeleteVisitor(int visitorId, Employee admin)
        {
            if (admin.Role != Roles.Admin)
            {
                throw new UnauthorizedAccessException("Only admins can delete visitors.");
            }

            var visitor = _dataRepository.Visitors.FirstOrDefault(v => v.VisitorsId == visitorId);
            if (visitor == null)
            {
                throw new InvalidOperationException("Visitor not found.");
            }

            _dataRepository.Visitors.Remove(visitor);
            return $"Visitor {visitor.VisitorsName} deleted successfully.";
        }

    }
}
