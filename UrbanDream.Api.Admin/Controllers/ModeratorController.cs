﻿using BazarJok.DataAccess.Models;
using BazarJok.Contracts.Attributes;
using BazarJok.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BazarJok.Contracts.ViewModels;
using BazarJok.Contracts.ViewModels.Users;
using BazarJok.DataAccess.Models.Users;
using BazarJok.DataAccess.Providers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

namespace BazarJok.Api.Admin.Controllers
{
    [Route("api/moderator/")]
    [AdminAuthorized(roles:AdminRole.Developer)]
    [EnableCors("Policy")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        private readonly AdminProvider _adminProvider;
        private readonly AdminAuthenticationService _authenticationService;

        public ModeratorController(AdminProvider adminProvider, AdminAuthenticationService authenticationService)
        {
            _adminProvider = adminProvider;
            _authenticationService = authenticationService;
        }

        [HttpGet("{id}")]        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _adminProvider.GetById(id);
            if (user is null)
                return NotFound("Support is not found");

            var supportViewModel = new AdminViewModel { Id = id, Login = user.Login, CreationDate = user.CreationDate.ToString("dd.MM.yyyy") };

            return Ok(supportViewModel);
        }

        [HttpGet]        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AdminViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var supports = await _adminProvider.Get(x=>x.Role == AdminRole.Support); 
            
            var supportViewModels = 
                supports.Select(support=> new AdminViewModel 
                {
                    Id = support.Id, 
                    Login = support.Login, 
                    CreationDate = support.CreationDate.ToString("dd.MM.yyyy")
                }).ToList();
            
            return Ok(supportViewModels);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(SupportViewModel support)
        {
            await _authenticationService.AddSupport(support.Login, support.Password);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Update(Guid id, SupportViewModel supportViewModel)
        {
            var support = await _adminProvider.GetById(id);
            if (support is null)
                return NotFound("Support is not found");
            var userClon = await _adminProvider.GetByLogin(supportViewModel.Login);
            if (userClon is not null && userClon.Id != support.Id)
            {
                return NotFound("Support login busy.");
            }

            support.Login = supportViewModel.Login;
            if (!String.IsNullOrEmpty(supportViewModel.Password))
            {
                support.PasswordHash = BCrypt.Net.BCrypt.HashPassword(supportViewModel.Password);
            }
            await _adminProvider.Edit(support);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var support = await _adminProvider.GetById(id);
            if (support is null)
                return NotFound("Support is not found");
            
            await _adminProvider.Remove(support);
            return Ok();
        }

    }

}
