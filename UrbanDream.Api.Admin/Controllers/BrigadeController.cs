using BazarJok.DataAccess.Models;
using BazarJok.Contracts.Attributes;
using BazarJok.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UrbanDream.Contracts.Dtos;
using BazarJok.Contracts.ViewModels;
using BazarJok.Contracts.ViewModels.Users;
using BazarJok.DataAccess.Models.Users;
using BazarJok.DataAccess.Providers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

namespace BazarJok.Api.Admin.Controllers
{
    [Route("api/brigade/")]
    [AdminAuthorized(roles:AdminRole.Support)]
    [EnableCors("Policy")]
    [ApiController]
    public class BrigadeController : ControllerBase
    {
        private readonly BrigadeProvider _brigadeProvider;

        public BrigadeController(BrigadeProvider brigadeProvider)
        {
            _brigadeProvider = brigadeProvider;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrigadeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Get(Guid id)
        {
            var brigade = await _brigadeProvider.GetById(id);
            if (brigade is null)
                return NotFound("Brigade is not found");

            var vendor = new BrigadeViewModel { Id = id, FirstName = brigade.FirstName, LastName = brigade.LastName, Login = brigade.Login, BrigadeName =  brigade.BrigadeName,
                BrigadePinsCount = brigade.BrigadePinsCount, BrigadeWorkAddress = brigade.BrigadeWorkAddress, BrigadeWorkersCount = brigade.BrigadeWorkersCount, Reports = brigade.Reports,
                CreationDate = brigade.CreationDate.ToShortDateString()
            };

            return Ok(vendor);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BrigadeViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            var brigades = await _brigadeProvider.GetAll(); 
            
            var brigadeViewModels = 
                brigades.Select(brigade => new BrigadeViewModel 
                {
                    Id = brigade.Id, 
                    FirstName = brigade.FirstName, 
                    LastName = brigade.LastName,
                    Login = brigade.Login, 
                    BrigadeName =  brigade.BrigadeName,
                    BrigadePinsCount = brigade.BrigadePinsCount,
                    BrigadeWorkAddress = brigade.BrigadeWorkAddress, 
                    BrigadeWorkersCount = brigade.BrigadeWorkersCount, 
                    Reports = brigade.Reports,
                    CreationDate = brigade.CreationDate.ToShortDateString()
                }).ToList();
            
            return Ok(brigadeViewModels);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(BrigadeCreationDto brigade)
        {
            await _brigadeProvider.Add(brigade);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Put(Guid id, PutBrigadeViewModel brigadeViewModel)
        {
            var brigade = await _brigadeProvider.GetById(id);
            if (brigade is null)
                return NotFound("Brigade is not found");
            var brigadeLogin = await _brigadeProvider.GetByLogin(brigadeViewModel.Login);
            if (brigadeLogin is not null && brigadeLogin.Id != brigade.Id)
            {
                return NotFound("Brigade login busy.");
            }
            brigade.FirstName = brigadeViewModel.FirstName;
            brigade.LastName = brigadeViewModel.LastName;
            brigade.Login = brigadeViewModel.Login;
            brigade.BrigadeName = brigadeViewModel.BrigadeName;
            brigade.BrigadeWorkAddress = brigadeViewModel.BrigadeWorkAddress;
            brigade.BrigadeWorkersCount = brigadeViewModel.BrigadeWorkersCount;

            if(!String.IsNullOrEmpty(brigadeViewModel.Password))
            {
                brigade.PasswordHash = BCrypt.Net.BCrypt.HashPassword(brigadeViewModel.Password);
            }
            await _brigadeProvider.Edit(brigade);
            return Ok();
        }

        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var brigade = await _brigadeProvider.GetById(id);
            if (brigade is null)
                return NotFound("Brigade is not found");
            
            await _brigadeProvider.Remove(brigade);
            return Ok();
        }

    }

}
