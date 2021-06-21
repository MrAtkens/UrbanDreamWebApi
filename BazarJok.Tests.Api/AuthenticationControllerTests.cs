using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BazarJok.Api.Admin;
using BazarJok.Tests.Api.infrastructure;
using Xunit;


namespace BazarJok.Tests.Api
{
    public class AuthenticationControllerTests : IntegrationTest<Startup>
    {
        [Fact]
        public async Task SignIn_FunctionalTest()
        {
            var response = await _testClient
                .PostAsJsonAsync(Routes.Admin.Authentication.SignIn, new
                {
                    Login = "dev",
                    Password = "123123"
                });

            var registrationResponse = await response.Content.ReadAsStringAsync();

            Assert.True(registrationResponse.Length > 0);
        }


        [Fact]
        public async Task GetUserData_FunctionalTest()
        {
            await AuthenticateAsync(Routes.Admin.Authentication.SignIn,
                new
                {
                    Login = "dev",
                    Password = "123123"
                });

            using var httpResponse = await _testClient.GetAsync(
                Routes.Admin.Authentication.GetUserData,
                HttpCompletionOption.ResponseHeadersRead);

            httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299

            var data = await httpResponse.Content
                .ReadAsAsync<Dictionary<string, string>>();
            
            Assert.Equal("dev", data["login"]);
        }
    }
}