using BazarJok.DataAccess.Models.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BazarJok.DataAccess.Models.Pins
{
    public class ProblemPin : PinsEntity
    {
        [Column(TypeName = "tinyint")]
        public PinState State { get; set; }
        public virtual Report Report { get; set; }
        //ID of moderator who moderated user created pin
        public Guid AcceptedModeratorId { get; set; }
        //Answer for user of moderator who moderated user created pin
        public string AcceptedModeratorAnswer { get; set; }
        //ID of moderator who moderated brigade report for pin
        public Guid ModeratedModeratorId { get; set; }
        //Answer for brigade of moderator who moderated brigade report for pin
        public string ModeratedModeratorAnswer { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public List<Image> Images { get; set; }

        public Guid BrigadeId { get; set; }
    }
}