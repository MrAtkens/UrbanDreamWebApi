using System.Collections.Generic;
using BazarJok.DataAccess.Models.Abstract;

namespace BazarJok.DataAccess.Models.Users
{
    public class Brigade : EntityUser
    {
        public string Login { get; set; }
        public string BrigadeName { get; set; }
        public string BrigadeWorkAddress { get; set; }
        public int BrigadeWorkersCount { get; set; }
        public List<Report> Reports { get; set; } 
        public BrigadeRole Role { get; set; }
        public int BrigadePinsCount { get; set; }
    }
}