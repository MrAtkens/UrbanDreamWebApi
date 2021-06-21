using System;
using System.Threading.Tasks;
using UrbanDream.Contracts.Dtos;
using BazarJok.DataAccess.Domain;
using BazarJok.DataAccess.Models;
using BazarJok.DataAccess.Models.Users;
using BazarJok.DataAccess.Providers.Abstract;

namespace BazarJok.DataAccess.Providers
{
    public class UserCrudProvider: UserProvider<User>
    {
        private readonly ApplicationContext _context;

        public UserCrudProvider(ApplicationContext context) : base(context)
        {
            _context = context;
        }


        public override async Task Add(UserCreationDto user)
        {
            await this.Add(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            });
        }
    }
}