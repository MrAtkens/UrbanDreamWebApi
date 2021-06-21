using System.Threading.Tasks;
using BazarJok.Services.Business;
using BazarJok.Services.Business.Abstract;
using Xunit;

namespace BazarJok.Tests.Services
{
    public class SmtpEmailSenderTests
    {
        [Fact]
        public async Task SendMessage_FunctionalTest()
        {
            IEmailSender service = new SmtpEmailSender(
                "bazarjok_tech_test@mail.ru", 
                "ouptiA-EPY22", 
                "smtp.mail.ru", 
                25);

            await service.SendMessage("gvo_step2018@mail.ru", "TEST", "TESTING");
        }
    }
}