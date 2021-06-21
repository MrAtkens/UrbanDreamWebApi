using BazarJok.DataAccess.Models.Abstract;

namespace BazarJok.DataAccess.Models.Users
{
    public class User : EntityUser
    { 
        public string Email { get; set; }
    }
}