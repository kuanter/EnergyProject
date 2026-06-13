using EnergyProject.Application.Interfaces;
using EnergyProject.Common;
using EnergyProject.Common.Models;
using Microsoft.AspNetCore.SignalR;

namespace EnergyProject.Application.Services.Stuff
{
    public class MeterBackgroundService : BackgroundService
    {
        private static readonly TimeSpan Interval = TimeSpan.FromSeconds(10);

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<MeterHub> _hub;
        private readonly ILogger<MeterBackgroundService> _logger;

        public MeterBackgroundService(IServiceScopeFactory scopeFactory, IHubContext<MeterHub> hub, ILogger<MeterBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _hub          = hub;
            _logger       = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessMetersAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while generating meter readings.");
                }
                await Task.Delay(Interval, stoppingToken);
            }
        }

        private async Task ProcessMetersAsync()
        {
            using var scope         = _scopeFactory.CreateScope();
            var meterReadingSvc     = scope.ServiceProvider.GetRequiredService<IMeterReadingService>();
            var meterSvc            = scope.ServiceProvider.GetRequiredService<IMeterService>();

            foreach (Meter meter in await meterSvc.GetActiveMeters())
            {
                meterReadingSvc.GenerateReading(meter.Id);
                var last = meterReadingSvc.GetLastMeterReading(meter.Id);

                await _hub.Clients.Group(meter.Id.ToString())
                    .SendAsync("ReceiveReading", new
                    {
                        id         = last.Id,
                        valueKWh   = last.ValueKWh,
                        createdAt  = last.CreatedAt
                    });
            }
        }
    }
}