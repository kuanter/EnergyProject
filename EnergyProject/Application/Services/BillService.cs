using EnergyProject.Application.Interfaces;
using EnergyProject.Areas.Client.Controllers;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;

namespace EnergyProject.Application.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly ILogger _logger;
        public BillService(IBillRepository billRepository, ILogger<HomeController> logger)
        {
            _billRepository = billRepository;
            _logger = logger;
        }

        public async Task<List<Bill>> GetBill(string id)
        {
            _logger.LogInformation("Used ShowBillController");
            var bills = await _billRepository.GetBillWithCardData(id);
            return bills;
        }
    }
}

