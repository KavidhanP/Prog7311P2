using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechMove.Models;
using Microsoft.AspNetCore.Hosting;
using TechMove.Repositories;

namespace TechMove.Controllers
{


    /* Microsoft 2023
 * Repository Pattern with ASP.NET Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/aspnet/core/fundamentals/repository-pattern>
 * Accessed: 18 April 2026
 */
    public class ContractsController : Controller
    {
        private readonly IContractRepository _contractRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IWebHostEnvironment _environment;

        public ContractsController(
            IContractRepository contractRepository,
            IClientRepository clientRepository,
            IWebHostEnvironment environment)
        {
            _contractRepository = contractRepository;
            _clientRepository = clientRepository;
            _environment = environment;
        }
        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var contracts = await _contractRepository.GetAllAsync();
            return View(contracts);
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _contractRepository.GetByIdAsync(id.Value);
            if (contract == null) return NotFound();

            return View(contract);
        }

        // GET: Contracts/Create
        public async Task<IActionResult> Create()
        {
            var clients = await _clientRepository.GetAllAsync();
            ViewData["ClientId"] = new SelectList(clients, "ClientId", "Name");

            ViewData["StatusList"] = new SelectList(new[] { "Draft", "Active", "On Hold", "Expired" });
            ViewData["ServiceLevelList"] = new SelectList(new[] { "Basic", "Standard", "Premium" });

            return View();
        }

        /* Microsoft 2024
 * File uploads in ASP.NET Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads>
 * Accessed: 22 April 2026
 */
        // POST: Contracts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,StartDate,EndDate,Status,ServiceLevel,ClientId")] Contract contract, IFormFile? signedAgreement)
        {
            if (signedAgreement != null)
            {
                // Validate PDF only
                if (Path.GetExtension(signedAgreement.FileName).ToLower() != ".pdf")
                {
                    ModelState.AddModelError("", "Only PDF files are allowed.");
                }
                else
                {
                    // Save with UUID filename to prevent overwrites
                    var fileName = Guid.NewGuid().ToString() + ".pdf";
                    var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await signedAgreement.CopyToAsync(stream);
                    }

                    contract.SignedAgreementPath = fileName;
                }
            }

            if (ModelState.IsValid)
            {
                await _contractRepository.AddAsync(contract);
                return RedirectToAction(nameof(Index));
            }

            var clients = await _clientRepository.GetAllAsync();
            ViewData["ClientId"] = new SelectList(clients, "ClientId", "Name", contract.ClientId);
            ViewData["StatusList"] = new SelectList(new[] { "Draft", "Active", "On Hold", "Expired" }, contract.Status);
            ViewData["ServiceLevelList"] = new SelectList(new[] { "Basic", "Standard", "Premium" }, contract.ServiceLevel);
            return View(contract);
        }
        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _contractRepository.GetByIdAsync(id.Value);
            if (contract == null) return NotFound();

            var clients = await _clientRepository.GetAllAsync();
            ViewData["ClientId"] = new SelectList(clients, "ClientId", "Name", contract.ClientId);
            ViewData["StatusList"] = new SelectList(new[] { "Draft", "Active", "On Hold", "Expired" });
            ViewData["ServiceLevelList"] = new SelectList(new[] { "Basic", "Standard", "Premium" });
            return View(contract);
        }

        /* Microsoft 2024
* Model Binding in ASP.NET Core
* Microsoft Learn
* <https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding>
* Accessed: 18 April 2026
*/

        // POST: Contracts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractId,StartDate,EndDate,Status,ServiceLevel,SignedAgreementPath,ClientId")] Contract contract)
        {
            if (id != contract.ContractId) return NotFound();

            if (ModelState.IsValid)
            {
                await _contractRepository.UpdateAsync(contract);
                return RedirectToAction(nameof(Index));
            }

            var clients = await _clientRepository.GetAllAsync();
            ViewData["ClientId"] = new SelectList(clients, "ClientId", "Name", contract.ClientId);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _contractRepository.GetByIdAsync(id.Value);
            if (contract == null) return NotFound();

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contractRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Contracts/Search
        public async Task<IActionResult> Search(DateTime? startDate, DateTime? endDate, string? status)
        {
            var contracts = await _contractRepository.SearchAsync(startDate, endDate, status);
            ViewData["StatusList"] = new SelectList(new[] { "Draft", "Active", "Expired", "On Hold" });
            return View(contracts);
        }
    }
}