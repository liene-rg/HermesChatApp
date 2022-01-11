using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using HermesChatApp.Properties.Models;

namespace SignalRChat.Hubs
{
    public class Chat 
    {
        public int Id { get; set; }

        public ICollection<Messages> Messages { get; set; }

        public ICollection<User> Users { get; set; }

        public ChatType Type { get; set; }



        

    }
}
