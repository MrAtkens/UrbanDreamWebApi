using System.ComponentModel.DataAnnotations;

namespace BazarJok.Contracts.ViewModels.Users
{
    public class LoginAdminViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }

}
