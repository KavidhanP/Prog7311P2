using Microsoft.EntityFrameworkCore;
using TechMove.Data;
using TechMove.Models;

namespace TechMove.Repositories
{
    public class ContractRepository : IContractRepository
    {
        /* Microsoft 2023
 * Repository Pattern with ASP.NET Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/aspnet/core/fundamentals/repository-pattern>
 * Accessed: 18 April 2026
 */
        private readonly TechMoveDbContext _context;

        /* Microsoft 2024
 * Entity Framework Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/ef/core/>
 * Accessed: 19 April 2026
 */

        public ContractRepository(TechMoveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contract>> GetAllAsync() =>
            await _context.Contracts.Include(c => c.Client).ToListAsync();

        public async Task<Contract?> GetByIdAsync(int id) =>
            await _context.Contracts.Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.ContractId == id);

        public async Task AddAsync(Contract contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contract contract)
        {
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();
            }
        }

        /* Microsoft 2024
 * Language Integrated Query (LINQ)
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/dotnet/csharp/linq>
 * Accessed: 20 April 2026
 */

        public async Task<IEnumerable<Contract>> SearchAsync(DateTime? startDate, DateTime? endDate, string? status)
        {
            var query = _context.Contracts.Include(c => c.Client).AsQueryable();

            if (startDate.HasValue)
                query = query.Where(c => c.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(c => c.EndDate <= endDate.Value);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(c => c.Status == status);

            return await query.ToListAsync();
        }
    }
}