using System;
using System.Threading.Tasks;
using BazarJok.DataAccess.Models;
using BazarJok.DataAccess.Providers;
using BazarJok.Tests.DataAccess.infrastructure.Helpers;
using Xunit;

namespace BazarJok.Tests.DataAccess
{
    public class AdminProviderTests
    {
        [Fact]
        public async Task ItShould_CreateAField()
        {
            var provider = new AdminProvider(new DbContextHelper().Context);
            
            var id = Guid.NewGuid();
            await provider.Add(new Admin
            {
                Id = id,
                Login = "TEST LOGIN",
                PasswordHash = "SOME HASH",
                Role = AdminRole.Support
            });

            var result = await provider.GetById(id);
            
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ItShould_UpdateAField()
        {
            var provider = new AdminProvider(new DbContextHelper().Context);
            var sut = AdminHelper.GetDeveloper();
            const string expected = "UPDATE TESTED";

            sut.Login = expected;
            await provider.Edit(sut);

            var result = await provider.FirstOrDefault(x => x.Id == sut.Id);
            
            Assert.Equal(result.Login, expected);
        }

        [Fact]
        public async Task ItShould_DeleteAField()
        {
            var provider = new AdminProvider(new DbContextHelper().Context);
            var sut = AdminHelper.GetDeveloper();

            await provider.Remove(sut);
            
            Assert.Null(await provider.FirstOrDefault(x => x.Id == sut.Id));
        }
    }
}