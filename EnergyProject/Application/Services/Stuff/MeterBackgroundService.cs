using EnergyProject.Application.Interfaces;
using EnergyProject.Common;
using EnergyProject.Common.Models;
using Microsoft.AspNetCore.SignalR;

namespace EnergyProject.Application.Services.Stuff
{
    public class MeterBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<MeterHub> _hubContext;

        public MeterBackgroundService(IServiceScopeFactory scopeFactory, IHubContext<MeterHub> hubContext)
        {
            _scopeFactory = scopeFactory;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var meterReadingService = scope.ServiceProvider.GetRequiredService<IMeterReadingService>();
                var meterService = scope.ServiceProvider.GetRequiredService<IMeterService>();

                foreach (Meter meter in await meterService.GetActiveMeters())
                {
                    meterReadingService.GenerateReading(meter.Id);

                    var lastMeterReading = meterReadingService.GetLastMeterReading(meter.Id);

                    await _hubContext.Clients.Group(meter.Id.ToString())
                        .SendAsync("ReceiveReading", new
                        {
                            id = lastMeterReading.Id,
                            valueKWh = lastMeterReading.ValueKWh,
                            createdAt = lastMeterReading.CreatedAt
                        });
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}