using BookMySeat.Domain.Data;
using BookMySeat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMySeat.Domain.Services
{
    public class AdminServiceForResources
    {


        private readonly DataRepository _dataRepository;

        public AdminServiceForResources(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        //Admin only Add the Resource
        public string AddResource(string resourceType, string resourceName, Guid adminId)
        {

            var admin = _dataRepository.Employees.FirstOrDefault(e => e.EmployeeId == adminId);
            if (admin == null || admin.Role != Roles.Admin)
            {
                throw new UnauthorizedAccessException("You do not have permission to add resources.");
            }

            if (string.IsNullOrEmpty(resourceType) || string.IsNullOrEmpty(resourceName))
            {
                throw new InvalidOperationException("Resource type and name cannot be empty.");
            }

            var newResource = new Resource(resourceType, resourceName);
            _dataRepository.Resources.Add(newResource);
            return $"Resource '{resourceName}' of type '{resourceType}' added successfully.";
        }





        //Admin Only Remove the Resource

        public string RemoveResource(Guid resourceId, Guid adminId)
        {

            var admin = _dataRepository.Employees.FirstOrDefault(e => e.EmployeeId == adminId);
            if (admin == null || admin.Role != Roles.Admin)
            {
                throw new UnauthorizedAccessException("You do not have permission to remove resources.");
            }
            var resource = _dataRepository.Resources.FirstOrDefault(r => r.ResourceId == resourceId);
            if (resource == null)
            {
                throw new InvalidOperationException("Resource not found.");
            }

            _dataRepository.Resources.Remove(resource);
            return $"Resource '{resource.ResourceName}' removed successfully.";
        }
    }
}
