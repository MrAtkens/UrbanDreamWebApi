using System;
using System.Collections.Generic;
using BazarJok.DataAccess.Models.Reports;

namespace BazarJok.Contracts.ViewModels.Reports
{
    public class ReportViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime WorkStartTime { get; set; }
        public ReportState State { get; set; }
        public Guid BrigadeId { get; set; }
        public Guid AcceptedModeratorId { get; set; }
        public virtual List<string> Images { get; set; }
        public virtual Guid Pin { get; set; }
    }
}
