using System.ComponentModel.DataAnnotations;

namespace BazarJok.Contracts.ViewModels.Users
{
    public class PutUserViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }

    }
}
