using BazarJok.DataAccess.Models.Abstract;
using BazarJok.DataAccess.Models.Pins;

namespace BazarJok.DataAccess.Models.System
{
    public class Image : Entity
    {
        public string Url { get; set; }
        public ProblemPin ProblemPin { get; set; }
    }
}
