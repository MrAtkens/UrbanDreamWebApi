using System;
using System.Collections.Generic;
using System.Text;
using BazarJok.DataAccess.Models.Abstract;
using BazarJok.DataAccess.Models.Pins;
using BazarJok.DataAccess.Models.Reports;
using BazarJok.DataAccess.Models.System;

namespace BazarJok.DataAccess.Models
{
    public class Report : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime WorkStartTime { get; set; }
        public ReportState State { get; set; }
        public Guid BrigadeId { get; set; }
        public Guid AcceptedModeratorId { get; set; }
        public virtual List<ReportImage> Images { get; set; }
        public virtual List<ProblemPin> Pins { get; set; }

    }
}
