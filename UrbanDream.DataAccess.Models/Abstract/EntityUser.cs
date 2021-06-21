namespace BazarJok.DataAccess.Models.Abstract
{
    public class EntityUser : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
    }
}