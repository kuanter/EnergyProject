using Microsoft.AspNetCore.SignalR;

namespace EnergyProject.Common
{
    public class MeterHub : Hub
    {
        public async Task Subscribe(string meterId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, meterId);
        }
    }
}
