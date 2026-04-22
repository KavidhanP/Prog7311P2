using TechMove.Models;

namespace TechMove.Repositories
{
    public interface IServiceRequestRepository // Defines the contract all ServiceRequest repositories must follow
    {
        Task<IEnumerable<ServiceRequest>> GetAllAsync(); // Retrieve all service requests from the database
        Task<ServiceRequest?> GetByIdAsync(int id); // Retrieve a single service request by its ID, returns null if not found
        Task AddAsync(ServiceRequest serviceRequest); // Add a new service request to the database
        Task UpdateAsync(ServiceRequest serviceRequest); // Update an existing service request in the database
        Task DeleteAsync(int id); // Delete a service request from the database by its ID
    }
}