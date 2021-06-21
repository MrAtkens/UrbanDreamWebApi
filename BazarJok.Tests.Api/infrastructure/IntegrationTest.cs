using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BazarJok.DataAccess.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BazarJok.Tests.Api.infrastructure
{
    public abstract class IntegrationTest<TStartup> where TStartup: class
    {
        protected readonly HttpClient _testClient;
        
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<TStartup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(ApplicationContext));
                        services.AddDbContext<ApplicationContext>(options => 
                            { options.UseInMemoryDatabase("TestDb"); });
                    });
                });
            
            _testClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync(string uri, object requestBody)
        {
            _testClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer",
                await GetJwtAsync(uri, requestBody));
        }

        private async Task<string> GetJwtAsync(string uri, object requestBody)
        {
            var response = await _testClient.PostAsJsonAsync(uri, requestBody);

            var registrationResponse = await response.Content.ReadAsStringAsync();
            return registrationResponse;
        }
    }
}