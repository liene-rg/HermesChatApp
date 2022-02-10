using System.Linq;
using System.Security.Claims;
using HermesChatTeamB_v3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HermesChatTeamB_v3.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;
        public RoomViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var chats = _context.ChatUsers
                    .Include(x => x.Chat)
                    .Where(x => x.UserId == userId 
                        && x.Chat.Type == ChatType.Room) // x is chat user
                    .Select(x => x.Chat)
                    .ToList();

                return View(chats);
            
        }   
    }
}   
