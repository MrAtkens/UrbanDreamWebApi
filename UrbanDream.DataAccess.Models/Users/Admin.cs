using System.ComponentModel.DataAnnotations.Schema;
using BazarJok.DataAccess.Models.Abstract;

namespace BazarJok.DataAccess.Models.Users
{
    public class Admin : EntityUser
    {
        public string Login { get; set; }

        [Column(TypeName = "tinyint")]
        public AdminRole Role { get; set; }
        public int ModeratedPinsCount { get; set; }
        public int AcceptedPinsCount { get; set; }
    }
}
