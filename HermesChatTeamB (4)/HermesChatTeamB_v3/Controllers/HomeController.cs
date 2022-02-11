using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HermesChatTeamB_v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using DLL.Data;
using DLL.Models;

namespace HermesChatTeamB_v3.Controllers
{
    // [Authorize] // need to be authorized to access this controller 
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;

            _context = context;
        }

        public IActionResult Index() //ok // gets all the chats that the current user participate
        {

            var chats = _context.Chats // a chat
                .Include(x => x.Users) // that includes all users
                .Where(x => !x.Users
                .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .ToList();

            return View(chats);
        }


        [HttpGet("{id}")]
        public ActionResult Chat(int id)
        {
            var chat = _context.Chats
                 .Include(x => x.Messages)
                 .FirstOrDefault(x => x.Id == id);
            return View(chat);

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


        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room,
            };

            chat.Users.Add(new ChatUser
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Admin
            });

            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Find([FromServices] ApplicationDbContext context) //ok
        {

            var users = context.Users
                .Where(x => x.Id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .ToList();

            return View(users);
        }

        public IActionResult Private() //ok
        {
            var chats = _context.Chats
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .Where(x => x.Type == ChatType.Private
                && x.Users.Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .ToList();

            return View(chats);
        }


        public async Task<IActionResult> CreatePrivateRoom(string userId)
        {
            var chat = new DLL.Models.Chat
            {
                Type = ChatType.Private
            };

            chat.Users.Add(new ChatUser
            {
                UserId = userId
            });

            chat.Users.Add(new ChatUser
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            });

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chat.Id });

        }

        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id) //ok
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Member
            };

            _context.ChatUsers.Add(chatUser);
            await _context.SaveChangesAsync();



            return RedirectToAction("Chat", "Home", new { id = id });
        }


        public IActionResult HomePage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


