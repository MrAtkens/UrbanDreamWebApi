using BazarJok.DataAccess.Models;
using BazarJok.DataAccess.Models.Users;

namespace UrbanDream.Contracts.Dtos
{
    public class AdminClaimsDto
    {
        public string Login { get; set; }
        public AdminRole Role { get; set; }
    }
}
