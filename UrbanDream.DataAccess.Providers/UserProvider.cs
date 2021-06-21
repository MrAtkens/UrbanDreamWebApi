using System;
using System.Threading.Tasks;
using UrbanDream.Contracts.Dtos;
using BazarJok.DataAccess.Domain;
using BazarJok.DataAccess.Models;
using BazarJok.DataAccess.Models.Abstract;
using BazarJok.DataAccess.Models.Users;
using BazarJok.DataAccess.Providers.Abstract;

namespace BazarJok.DataAccess.Providers
{
    public abstract class UserProvider<T> : EntityProvider<ApplicationContext, T, Guid> where T: User
    {
        private readonly ApplicationContext _context;

        protected UserProvider(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<T> GetByEmailOrPhone(string emailOrPhone)
        {
            var user = await this.FirstOrDefault(x => 
                x.Email.ToLower().Equals(emailOrPhone.ToLower()));

            return user; //?? throw new ArgumentException("User is not found");
        }

        public abstract Task Add(UserCreationDto user);
    }
}