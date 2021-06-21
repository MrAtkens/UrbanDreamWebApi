using System.ComponentModel.DataAnnotations;

namespace BazarJok.Contracts.ViewModels.Users
{
    public class PutBrigadeViewModel
    {
        [Required]
        public string Login { get; set; }
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string BrigadeName { get; set; }
        [Required]
        public string BrigadeWorkAddress { get; set; }
        public int BrigadeWorkersCount { get; set; }
    }
}