using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BazarJok.Contracts.ViewModels.Pins
{
    public class ProblemPinViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int State { get; set; }
        public int CountOfWatchings { get; set; }
        public Guid UserId { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Images { get; set; }
        public string CreationDate { get; set; }
        public Guid BrigadeId { get; set; }
        public Guid ModeratedModeratorId { get; set; }
        public Guid AcceptedModeratorId { get; set; }
        public string ModeratedModeratorAnswer { get; set; }
        public string AcceptedModeratorAnswer { get; set; }
    }
}
