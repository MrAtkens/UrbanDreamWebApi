using System.ComponentModel.DataAnnotations;

namespace UrbanDream.Contracts.Dtos
{
    public class BrigadeCreationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, MinLength(4), MaxLength(20)]
        public string Login { get; set; }
        [Required, MinLength(8), MaxLength(40)]
        public string PasswordHash { get; set; }
        [Required, MinLength(4), MaxLength(80)]
        public string BrigadeName { get; set; }
        [Required, MinLength(4), MaxLength(100)]
        public string BrigadeWorkAddress { get; set; }
        [Required]
        public int BrigadeWorkersCount { get; set; }
    }
}