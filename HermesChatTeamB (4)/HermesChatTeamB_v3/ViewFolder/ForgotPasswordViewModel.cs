using System.ComponentModel.DataAnnotations;
using DLL.Models;

namespace HermesChatTeamB_v3.ViewFolder
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}