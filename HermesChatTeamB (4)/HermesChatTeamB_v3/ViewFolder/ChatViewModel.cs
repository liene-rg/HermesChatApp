using Microsoft.AspNetCore.Mvc;

namespace HermesChatTeamB_v3.ViewFolder
{
    public class ChatViewModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
