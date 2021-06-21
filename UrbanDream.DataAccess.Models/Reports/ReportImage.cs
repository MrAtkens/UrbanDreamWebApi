using System.Collections.Generic;
using BazarJok.DataAccess.Models.Abstract;
using BazarJok.DataAccess.Models.Pins;

namespace BazarJok.DataAccess.Models.System
{
    public class ReportImage : Entity
    {
        public string Url { get; set; }
        public virtual List<Report> Reports { get; set; }
    }
}