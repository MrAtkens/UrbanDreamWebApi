using System.ComponentModel.DataAnnotations;
using BazarJok.DataAccess.Models.Pins;

namespace BazarJok.Contracts.ViewModels.Pins
{
    public class PinStateViewModel
    {
        [Required, MaxLength(1200), MinLength(10)]
        public string Answer { get; set; }
        [Required]
        public PinState State { get; set; }
    }
}