using BazarJok.DataAccess.Models;
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
    [Route("api/user/")]
    [AdminAuthorized(roles:AdminRole.Support)]
    [EnableCors("Policy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserCrudProvider _userCrudProvider;

        public UserController(UserCrudProvider userCrudProvider)
        {
            _userCrudProvider = userCrudProvider;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userCrudProvider.GetById(id);
            if (user is null)
                return NotFound("User is not found");

            var customer = new UserViewModel { Id = id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email,
                CreationDate = user.CreationDate.ToShortDateString()
            };

            return Ok(customer);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userCrudProvider.GetAll(); 
            
            var usersViewModels = 
                users.Select(user => new UserViewModel 
                {
                    Id = user.Id, 
                    FirstName = user.FirstName, 
                    LastName = user.LastName,
                    Email = user.Email,
                    CreationDate = user.CreationDate.ToShortDateString()
                }).ToList();
            
            return Ok(usersViewModels);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Put(Guid id, PutAdminUserViewModel userViewModel)
        {
            var user = await _userCrudProvider.GetById(id);
            if (user is null)
                return NotFound("User is not found");
            var userClone = await _userCrudProvider.GetByEmailOrPhone(userViewModel.Email);
            if(userClone is not null && userClone.Id != user.Id)
            {
                return NotFound("User email busy.");
            }
            
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;
            if (!String.IsNullOrEmpty(userViewModel.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userViewModel.Password);
            }
            await _userCrudProvider.Edit(user);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userCrudProvider.GetById(id);
            if (user is null)
                return NotFound("User is not found");
            
            await _userCrudProvider.Remove(user);
            return Ok();
        }

    }

}
