using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using HermesChatApp.Properties.Models;
namespace SignalRChat.Hubs
{
    public enum ChatType
    {
        Room,
        Private
    }
}
