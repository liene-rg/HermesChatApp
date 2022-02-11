using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using DLL.Repository;

namespace HermesChatTeamB_v3.Controllers
{
    public class ClaimPrincipalIdController : Controller
    {
        protected string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}