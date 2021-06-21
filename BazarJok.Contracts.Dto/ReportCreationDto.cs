using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BazarJok.DataAccess.Models.Reports;
using BazarJok.DataAccess.Models.System;

namespace UrbanDream.Contracts.Dtos
{
    public class ReportCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required, MaxLength(1200), MinLength(300)]
        public string Description { get; set; }
        [Required]
        public DateTime WorkStartTime { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public Guid BrigadeId { get; set; }
        [Required]
        public Guid AcceptedModeratorId { get; set; }
        [Required]
        public virtual List<string> Images { get; set; }
        [Required]
        public virtual List<Guid> Pins { get; set; }
    }
}