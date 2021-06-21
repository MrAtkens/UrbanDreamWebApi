using BazarJok.DataAccess.Domain;
using BazarJok.DataAccess.Providers.Abstract;
using System;
using System.Threading.Tasks;
using BazarJok.DataAccess.Models.Users;

namespace BazarJok.DataAccess.Providers
{
    public class AdminProvider : EntityProvider<ApplicationContext, Admin, Guid>
    {
        private readonly ApplicationContext _context;

        public AdminProvider(ApplicationContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Admin> GetByLogin(string login)
        {
            return await FirstOrDefault(x => x.Login.Equals(login)); //?? throw new ArgumentException("Admin is not found");
        }
    }
}
