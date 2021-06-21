using System;
using System.Collections.Generic;
using BazarJok.DataAccess.Models;

namespace BazarJok.Contracts.ViewModels.Users
{
    public class BrigadeViewModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreationDate { get; set; }
        public string BrigadeName { get; set; }
        public string BrigadeWorkAddress { get; set; }
        public int BrigadeWorkersCount { get; set; }
        public List<Report> Reports { get; set; } 
        public int BrigadePinsCount { get; set; }
    }
}