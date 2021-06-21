using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BazarJok.Contracts.Options;
using BazarJok.Services.Business.Abstract;
using Microsoft.Extensions.Options;

namespace BazarJok.Services.Business
{
    public class SmscSmsSender : ISmsSender
    {
        private readonly SmscDataOptions _options;

        public SmscSmsSender(IOptions<SmscDataOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendMessage(string phoneNumber, string message)
        {
            using var client = new HttpClient();
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get,
                new Uri(
                    $"https://smsc.kz/sys/send.php?" +
                    $"login={_options.Login}&" +
                    $"psw={_options.Password}&" +
                    $"phones={phoneNumber}&" +
                    $"mes={message}")));
        }
    }
}