using System;
using System.Threading.Tasks;
using UrbanDream.Contracts.Dtos;
using BazarJok.DataAccess.Domain;
using BazarJok.DataAccess.Models.Users;
using BazarJok.DataAccess.Providers.Abstract;

namespace BazarJok.DataAccess.Providers
{
    public class BrigadeProvider : EntityProvider<ApplicationContext, Brigade, Guid>
    {
        private readonly ApplicationContext _context;

        public BrigadeProvider(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Brigade> GetByLogin(string login)
        {
            return await FirstOrDefault(x => x.Login.Equals(login)); //?? throw new ArgumentException("Brigade is not found");
        }

        public async Task Add(BrigadeCreationDto brigade)
        {
            await this.Add(new Brigade
            {
                FirstName = brigade.FirstName,
                LastName = brigade.LastName,
                Login = brigade.Login,
                PasswordHash = brigade.PasswordHash,
                BrigadeName = brigade.BrigadeName,
                BrigadeWorkAddress = brigade.BrigadeWorkAddress,
                BrigadeWorkersCount = brigade.BrigadeWorkersCount
            });
        }

    }
}