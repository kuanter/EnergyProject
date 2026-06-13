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

        public MeterBackgroundService(IServiceScopeFactory scopeFactory, IHubContext<MeterHub> hub)
        {
            _scopeFactory = scopeFactory;
            _hub          = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessMetersAsync();
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