using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechMove.Models;
using TechMove.Repositories;
using TechMove.States;
using System.Text.Json;

namespace TechMove.Controllers
{
    public class ServiceRequestsController : Controller
    {

        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceRequestsController(
            IServiceRequestRepository serviceRequestRepository,
            IContractRepository contractRepository,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _serviceRequestRepository = serviceRequestRepository;
            _contractRepository = contractRepository;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        /* Microsoft 2023
 * Repository Pattern with ASP.NET Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/aspnet/core/fundamentals/repository-pattern>
 * Accessed: 18 April 2026
 */

        // GET: ServiceRequests
        public async Task<IActionResult> Index()
        {
            var serviceRequests = await _serviceRequestRepository.GetAllAsync();
            return View(serviceRequests);
        }

        // GET: ServiceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _serviceRequestRepository.GetByIdAsync(id.Value);
            if (serviceRequest == null) return NotFound();

            return View(serviceRequest);
        }

        // GET: ServiceRequests/Create
        public async Task<IActionResult> Create()
        {
            var contracts = await _contractRepository.GetAllAsync();
            ViewData["ContractId"] = new SelectList(contracts, "ContractId", "ContractId");
            ViewData["StatusList"] = new SelectList(new[] { "Pending", "Active", "Completed", "Cancelled" });
            return View();
        }

        // POST: ServiceRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceRequestId,Description,Cost,CostUSD,Status,ContractId")] ServiceRequest serviceRequest)
        {
            var contract = await _contractRepository.GetByIdAsync(serviceRequest.ContractId);

            if (contract == null)
            {
                ModelState.AddModelError("", "Contract not found.");
            }
            else
            {
                var state = ContractStateFactory.GetState(contract.Status);
                if (!state.CanCreateServiceRequest())
                {
                    ModelState.AddModelError("", state.GetBlockedReason());
                }
            }

            if (ModelState.IsValid)
            {
                await _serviceRequestRepository.AddAsync(serviceRequest);
                return RedirectToAction(nameof(Index));
            }

            var contracts = await _contractRepository.GetAllAsync();
            ViewData["ContractId"] = new SelectList(contracts, "ContractId", "ContractId", serviceRequest.ContractId);
            ViewData["StatusList"] = new SelectList(new[] { "Pending", "Active", "Completed", "Cancelled" }, serviceRequest.Status);
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _serviceRequestRepository.GetByIdAsync(id.Value);
            if (serviceRequest == null) return NotFound();

            var contracts = await _contractRepository.GetAllAsync();
            ViewData["ContractId"] = new SelectList(contracts, "ContractId", "ContractId", serviceRequest.ContractId);
            return View(serviceRequest);
        }

        /* Microsoft 2024
 * Model Binding in ASP.NET Core
 * Microsoft Learn
 * <https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding>
 * Accessed: 18 April 2026
 */

        // POST: ServiceRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceRequestId,Description,Cost,Status,ContractId")] ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.ServiceRequestId) return NotFound();

            if (ModelState.IsValid)
            {
                await _serviceRequestRepository.UpdateAsync(serviceRequest);
                return RedirectToAction(nameof(Index));
            }

            var contracts = await _contractRepository.GetAllAsync();
            ViewData["ContractId"] = new SelectList(contracts, "ContractId", "ContractId", serviceRequest.ContractId);
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _serviceRequestRepository.GetByIdAsync(id.Value);
            if (serviceRequest == null) return NotFound();

            return View(serviceRequest);
        }

        // POST: ServiceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceRequestRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        /* ExchangeRate-API 2024
 * ExchangeRate-API Documentation
 * ExchangeRate-API
 * <https://www.exchangerate-api.com/docs/c-sharp-currency-api>
 * Accessed: 20 April 2026
 */

        // GET: ServiceRequests/GetExchangeRate
        public async Task<IActionResult> GetExchangeRate()
        {
            try
            {
                var apiKey = _configuration["ExchangeRate:ApiKey"];
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(
                    $"https://v6.exchangerate-api.com/v6/{apiKey}/pair/USD/ZAR");

                var json = JsonDocument.Parse(response);
                var rate = json.RootElement
                    .GetProperty("conversion_rate")
                    .GetDecimal();

                return Json(new { rate });
            }
            catch
            {
                return Json(new { rate = 0, error = "Could not fetch exchange rate." });
            }
        }
    }
}
