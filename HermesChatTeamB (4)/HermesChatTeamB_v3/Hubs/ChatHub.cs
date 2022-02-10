using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HermesChatTeamB_v3.Hubs
{
    public class ChatHub : Hub
    {

        public string GetConnectionId() //connection id with signalr 
        {
            return Context.ConnectionId;
        }
        public Task JoinRoom(string roomId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomId);

        }

        public Task LeaveRoom(string roomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

        }


    }
}
