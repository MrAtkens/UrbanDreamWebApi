using System.Threading.Tasks;
using BazarJok.Services.Business;
using BazarJok.Services.Business.Abstract;
using Xunit;

namespace BazarJok.Tests.Services
{
    public class MobizonSmsSenderTests
    {
        [Fact]
        public async Task SendMessage_FunctionalTest()
        {
            //ISmsSender sender = new MobizonSmsSender("api");

           // await sender.SendMessage("number", "TEST");
        }
    }
}