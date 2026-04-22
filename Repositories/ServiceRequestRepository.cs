using Microsoft.EntityFrameworkCore;
using TechMove.Data;
using TechMove.Models;

namespace TechMove.Repositories
{
    /* Microsoft 2023
 * Repository Pattern with ASP.NET Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/aspnet/core/fundamentals/repository-pattern>
 * Accessed: 18 April 2026
 */

    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly TechMoveDbContext _context;

        public ServiceRequestRepository(TechMoveDbContext context)
        {
            _context = context;
        }


        /* Microsoft 2024
         * Entity Framework Core
         * Microsoft Learn
         * <https://learn.microsoft.com/en-us/ef/core/>
         * Accessed: 22 April 2026
         */

        public async Task<IEnumerable<ServiceRequest>> GetAllAsync() =>
            await _context.ServiceRequests.Include(s => s.Contract).ToListAsync();

        public async Task<ServiceRequest?> GetByIdAsync(int id) =>
            await _context.ServiceRequests.Include(s => s.Contract)
                .FirstOrDefaultAsync(s => s.ServiceRequestId == id);

        public async Task AddAsync(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Add(serviceRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Update(serviceRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sr = await _context.ServiceRequests.FindAsync(id);
            if (sr != null)
            {
                _context.ServiceRequests.Remove(sr);
                await _context.SaveChangesAsync();
            }
        }
    }
}