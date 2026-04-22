using Microsoft.AspNetCore.Mvc;
using TechMove.Models;
using TechMove.Repositories;

namespace TechMove.Controllers
{
    /* Microsoft 2023
 * Repository Pattern with ASP.NET Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/aspnet/core/fundamentals/repository-pattern>
 * Accessed: 18 April 2026
 */
    public class ClientsController : Controller
    {
        /* Microsoft 2024
 * Dependency injection in ASP.NET Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection>
 * Accessed: 18 April 2026
 */

        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var clients = await _clientRepository.GetAllAsync();
            return View(clients);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null) return NotFound();

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,Name,ContactDetails,Region")] Client client)
        {
            if (ModelState.IsValid)
            {
                await _clientRepository.AddAsync(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null) return NotFound();

            return View(client);
        }

        /* Microsoft 2024
 * Model Binding in ASP.NET Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding>
 * Accessed: 18 April 2026
 */
        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,Name,ContactDetails,Region")] Client client)
        {
            if (id != client.ClientId) return NotFound();

            if (ModelState.IsValid)
            {
                await _clientRepository.UpdateAsync(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null) return NotFound();

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}