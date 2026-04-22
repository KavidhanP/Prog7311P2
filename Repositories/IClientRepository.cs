using TechMove.Models;

namespace TechMove.Repositories
{
    public interface IClientRepository // Defines the contract all Client repositories must follow
    {
        Task<IEnumerable<Client>> GetAllAsync(); // Retrieve all clients from the database
        Task<Client?> GetByIdAsync(int id); // Retrieve a single client by their ID, returns null if not found
        Task AddAsync(Client client); // Add a new client to the database
        Task UpdateAsync(Client client); // Update an existing client in the database
        Task DeleteAsync(int id); // Delete a client from the database by their ID
    }
}