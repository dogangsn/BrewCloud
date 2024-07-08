using Microsoft.AspNetCore.SignalR;

namespace VetSystems.Hubs.Api.Hubs
{
    public class ServiceHub : Hub
    {
        public static int viewCount { get; set; } = 0;

        public async Task NotfyWahtching()
        {
            viewCount++;
            await Clients.All.SendAsync("viewCountUpdate", viewCount);
        }


    }
}
