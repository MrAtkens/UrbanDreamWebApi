using System.Linq;
using BazarJok.Tests.DataAccess.infrastructure.Helpers;
using Xunit;

namespace BazarJok.Tests.DataAccess
{
    public class ContextInitializationTests
    {
        [Fact]
        public void ItShould_HasOneAdminWithSpecialId()
        {
            var id = AdminHelper.GetDeveloper().Id;
            var context = new DbContextHelper().Context;

            var result = context.Admins.FirstOrDefault(x => x.Id == id);
            
            Assert.NotNull(result);
        }
    }
}