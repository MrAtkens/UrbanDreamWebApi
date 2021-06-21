using System.Collections.Generic;
using BazarJok.DataAccess.Models.Abstract;
using BazarJok.DataAccess.Models.Pins;

namespace BazarJok.DataAccess.Models.System
{
    public class Tag : Entity
    {
        public string Title { get; set; }
        
        public virtual List<ProblemPin> Pins { get; set; }
    }
}
