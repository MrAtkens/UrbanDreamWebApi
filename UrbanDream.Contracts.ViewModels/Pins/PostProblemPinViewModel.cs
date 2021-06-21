using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BazarJok.Contracts.ViewModels.Pins
{
    public class PostProblemPinViewModel
    {
        [Required, MaxLength(40), MinLength(10)]
        public string Title { get; set; }
        [Required, MaxLength(500), MinLength(40)]
        public string Description { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public List<string> Tags { get; set; }
        [Required]
        public List<string> Images { get; set; }
    }
}
