using System.Threading.Tasks;

namespace BazarJok.Services.Business.Abstract
{
    public interface ISmsSender
    {
        Task SendMessage(string phoneNumber, string text);
    }
}