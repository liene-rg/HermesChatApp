using HermesChatTeamB_v3.Hubs;
using HermesChatTeamB_v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HermesChatTeamB_v3.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _chat;

        private protected ApplicationDbContext _context;
        public ChatController(IHubContext<ChatHub> chat, ApplicationDbContext context)
        {
            _chat = chat;
            _context = context;
        }

        [HttpPost("[action]/{connectionId}/roomId")]

        public async Task<IActionResult> JoinRoom(string connectionId, string roomId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomId);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/roomId")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomId)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var Message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();


            return RedirectToAction("Chat", new { id = chatId });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(
            int chatId,
            string message,
            string roomName,
            [FromServices] ApplicationDbContext context)
        {
            var Message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };

            context.Messages.Add(Message);
            await context.SaveChangesAsync();

            await _chat.Clients.Group(roomName)
                .SendAsync("ReceiveMessage", new
                {
                    Text = Message.Text,
                    Name = Message.Name,
                    TimeStamp = Message.Timestamp.ToString("dd/MM/yyyyy hh:mm:ss")
                });
            return Ok();
        }
    }
}
