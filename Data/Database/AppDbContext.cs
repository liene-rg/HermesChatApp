using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SignalRChat.Hubs;
using HermesChatApp.Properties.Models;

namespace HermesChatApp.Data.Database
{
    public class AppDbContext :IdentityDbContext<User>  //<user> recognizes what user creating
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Chat> Chats { get; set; }
        public DbSet<Messages> Messages { get; set; }


    }
}
