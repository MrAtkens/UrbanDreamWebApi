using BazarJok.Contracts.ViewModels;
using BazarJok.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using BazarJok.Contracts.ViewModels.Users;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

namespace BazarJok.Api.Admin.Controllers
{
    [Route("api/auth/[action]")]
    [EnableCors("Policy")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly BrigadeAuthenticationService _brigadeAuthenticationService;

        public AuthenticationController(BrigadeAuthenticationService brigadeAuthenticationService)
        {
            _brigadeAuthenticationService = brigadeAuthenticationService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SignIn(LoginAdminViewModel loginUserViewModel)
        {
            try
            {
                var token = await _brigadeAuthenticationService
                    .Authenticate(loginUserViewModel.Login, loginUserViewModel.Password);
                
                return token != null ? 
                    (IActionResult) Ok(token) : Unauthorized();
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataAccess.Models.Users.Brigade))]
        public async Task<IActionResult> GetUserData()
        {
            var admin = await _brigadeAuthenticationService
                .GetBrigadeByHeaders(Request.Headers[HeaderNames.Authorization].ToArray());
                
            return Ok(new { 
                admin.Id,
                admin.Login,
                admin.Role
            });
            
        }

    }

}
