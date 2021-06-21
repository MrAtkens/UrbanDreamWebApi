using System;
using BazarJok.DataAccess.Models.Abstract;

namespace BazarJok.DataAccess.Models.System
{
    public class Ticket : Entity
    {
        public string Topic { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public Guid ModeratorId { get; set; }

    }
}