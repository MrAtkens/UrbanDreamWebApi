using System;
using BazarJok.DataAccess.Models.Abstract;

namespace BazarJok.DataAccess.Models.Pins
{
    public class PinsEntity : Entity
    {
        public string Title { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Description { get; set; }
        public int CountOfWatchings { get; set; }
        public Guid UserId { get; set; }
        public double Raiting { get; set; }
    }
}
