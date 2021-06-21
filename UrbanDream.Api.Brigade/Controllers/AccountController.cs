using BazarJok.Contracts.ViewModels;
using BazarJok.DataAccess.Providers;
using BazarJok.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BazarJok.Contracts.ViewModels.Users;
using Microsoft.AspNetCore.Cors;

namespace BazarJok.Api.Vendor.Controllers
{
    [Route("api/account/")]
    [ApiController]
    [EnableCors("Policy")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly BrigadeProvider _brigadeProvider;
        private readonly BrigadeAuthenticationService _brigadeAuthenticationService;

        public AccountController(BrigadeProvider brigadeProvider,
            BrigadeAuthenticationService brigadeAuthenticationService)
        {
            _brigadeProvider = brigadeProvider;
            _brigadeAuthenticationService = brigadeAuthenticationService;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        public async Task<IActionResult> Get()
        {
            var brigade = await _brigadeAuthenticationService
                .GetBrigadeByHeaders(Request.Headers[HeaderNames.Authorization].ToArray());

            var vendorViewModel = new BrigadeViewModel()
            {
                Id = brigade.Id, 
                FirstName = brigade.FirstName, 
                LastName = brigade.LastName, 
                Login = brigade.Login,
                BrigadeName = brigade.BrigadeName,
                BrigadeWorkAddress = brigade.BrigadeWorkAddress,
                BrigadePinsCount = brigade.BrigadePinsCount,
                BrigadeWorkersCount = brigade.BrigadeWorkersCount,
                CreationDate = brigade.CreationDate.ToString("d"),
                Reports = brigade.Reports,
            };

            return Ok(vendorViewModel);
        }

        

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(PutBrigadeViewModel brigadeViewModel)
        {
            var vendor = await _brigadeAuthenticationService
                .GetBrigadeByHeaders(Request.Headers[HeaderNames.Authorization].ToArray());

            vendor.FirstName = brigadeViewModel.FirstName;
            vendor.LastName = brigadeViewModel.LastName;
            vendor.BrigadeName = brigadeViewModel.BrigadeName;
            
            if (!String.IsNullOrEmpty(brigadeViewModel.Password))
            {
                vendor.PasswordHash = BCrypt.Net.BCrypt.HashPassword(brigadeViewModel.Password);
            }
            
            await _brigadeProvider.Edit(vendor);
            return Ok();
        }


    }
}
