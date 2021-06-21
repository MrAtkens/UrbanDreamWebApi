using System.Threading.Tasks;

namespace BazarJok.Services.Business.Abstract
{
    public interface IEmailSender
    {
        Task SendMessage(string receiver, string title, string body);
    }
}