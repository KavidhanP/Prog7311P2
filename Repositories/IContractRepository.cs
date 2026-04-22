using TechMove.Models;

namespace TechMove.Repositories
{
    public interface IContractRepository // Defines the contract all Contract repositories must follow
    {
        Task<IEnumerable<Contract>> GetAllAsync(); // Retrieve all contracts from the database
        Task<Contract?> GetByIdAsync(int id); // Retrieve a single contract by its ID, returns null if not found
        Task AddAsync(Contract contract); // Add a new contract to the database
        Task UpdateAsync(Contract contract); // Update an existing contract in the database
        Task DeleteAsync(int id); // Delete a contract from the database by its ID
        Task<IEnumerable<Contract>> SearchAsync(DateTime? startDate, DateTime? endDate, string? status); // Search contracts by date range and status using LINQ
    }
}