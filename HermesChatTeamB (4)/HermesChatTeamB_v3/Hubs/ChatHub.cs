using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HermesChatTeamB_v3.Hubs
{
    public class ChatHub : Hub
    {
        public Task JoinRoom(string roomId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public Task LeaveRoom(string roomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Receive", message, Context.User.Identity.Name);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} is online");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} left the chat");
            await base.OnDisconnectedAsync(exception);
        }
    }
}