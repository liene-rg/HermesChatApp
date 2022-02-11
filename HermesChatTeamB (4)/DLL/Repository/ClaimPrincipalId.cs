using System.Security.Claims;

namespace DLL.Repository
{
    public static class ClaimsPrincipalId
    {
        public static string GetUserId(this ClaimsPrincipal @this)
        {
            return @this.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}