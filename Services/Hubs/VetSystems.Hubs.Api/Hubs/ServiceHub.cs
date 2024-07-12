using Microsoft.AspNetCore.SignalR;

namespace VetSystems.Hubs.Api.Hubs
{
    public class ServiceHub : Hub
    {

        static List<GroupClient> groupClients = new List<GroupClient>();
        public static int viewCount { get; set; } = 0;

        public async Task NotfyWahtching()
        {
            viewCount++;
            await Clients.All.SendAsync("viewCountUpdate", viewCount);
        }
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task AddGroup(string groupName)
        {
            GroupClient groupClient = groupClients.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId && c.GroupName == groupName);
            if (groupClient == null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                groupClients.Add(new GroupClient
                {
                    ConnectionId = Context.ConnectionId,
                    GroupName = groupName
                });
            }
        }

        //public async Task RemoveFromGroup(string property)
        //{
        //    groupClients.Clients.RemoveAll(r => r.ClientId == Context.ConnectionId);
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, property);
        //}
        //public async override Task OnConnectedAsync()
        //{
        //    await Clients.Caller.SendAsync("allClients", clients);
        //    clients.Add(Context.ConnectionId);
        //    await Clients.Caller.SendAsync("connected", Context.ConnectionId);
        //    await Clients.Others.SendAsync("allClients", clients);
        //    await Clients.All.SendAsync("groupNames", groupsClient.Distinct());
        //}
    }

    public class GroupClient
    {
        public string ConnectionId { get; set; }
        public string GroupName { get; set; }
    }
}
