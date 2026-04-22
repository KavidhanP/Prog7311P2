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
    public class ClientRepository : IClientRepository
    {
        private readonly TechMoveDbContext _context;

        public ClientRepository(TechMoveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync() =>
            await _context.Clients.ToListAsync();

        public async Task<Client?> GetByIdAsync(int id) =>
            await _context.Clients.FindAsync(id);

        public async Task AddAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
        /* Microsoft 2024
 * Entity Framework Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/ef/core/>
 * Accessed: 19 April 2026
 */
        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}