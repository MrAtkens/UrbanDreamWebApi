using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BazarJok.DataAccess.Models.Reports;

namespace BazarJok.Contracts.ViewModels.Reports
{
    public class PostReportViewModel
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(1200)]
        public string Description { get; set; }
        [Required]
        public DateTime WorkStartTime { get; set; }
        [Required]
        public virtual List<string> Images { get; set; }
        [Required]
        public virtual Guid Pin { get; set; }

    }
}
