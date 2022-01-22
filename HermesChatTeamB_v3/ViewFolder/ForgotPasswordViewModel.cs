using System.ComponentModel.DataAnnotations;

namespace HermesChatTeamB_v3.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}