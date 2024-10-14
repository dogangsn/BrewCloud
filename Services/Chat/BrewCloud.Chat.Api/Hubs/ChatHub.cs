using Microsoft.AspNetCore.SignalR;

namespace BrewCloud.Chat.Api.Hubs
{
    public class ChatHub : Hub
    {
        //public async Task SendMessageToUser(MessageForReturnDto message)
        //{
        //    var senderId = message.SenderId.ToString();
        //    var recipientId = message.RecipientId.ToString();
        //    await Clients.Users(senderId, recipientId).SendAsync("Chat", message);
        //}
    }
}
