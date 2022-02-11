using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DLL.Models
{
    public class User : IdentityUser

    {
        public User() : base()
        {
            Chats = new List<ChatUser>();
        }
        public ICollection<ChatUser> Chats { get; set; }
        
    }

}