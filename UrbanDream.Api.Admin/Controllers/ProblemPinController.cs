using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BazarJok.Contracts.ViewModels.Pins;
using BazarJok.DataAccess.Models.Pins;
using BazarJok.DataAccess.Providers;
using BazarJok.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace BazarJok.Api.Admin.Controllers
{
    [EnableCors("Policy")]
    [Route("api/pin/")]
    [ApiController]
    [Authorize]
    public class PinController : ControllerBase
    {
        private readonly AdminAuthenticationService _authenticationService;
        private readonly ProblemPinProvider _problemPinProvider;
        private readonly ImageProvider _imageProvider;
        private readonly TagProvider _tagProvider;

        public PinController(AdminAuthenticationService authenticationService, ProblemPinProvider problemPinProvider, ImageProvider imageProvider,
            TagProvider tagProvider)
        {
            _authenticationService = authenticationService;
            _problemPinProvider = problemPinProvider;
            _imageProvider = imageProvider;
            _tagProvider = tagProvider;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProblemPinViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetByPinId(Guid id)
        {
    
            var pins = await _problemPinProvider.GetFullData(id);
            if (pins is null)
                return NotFound("Problem pin is not found");
            return Ok(pins);
        }

        [HttpGet("/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProblemPinViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var pins = await _problemPinProvider.GetById(id);

            if (pins is null)
                return NotFound("Problem pins is not found");

            return Ok(pins);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProblemPinViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAll()
        {

            var pins = await _problemPinProvider.GetAll();

            if (pins is null)
                return NotFound("Problem pins is not found");

            return Ok(pins);
        }
        
        [HttpGet("/api/pin/moderating")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProblemPinViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAllModeratingPins()
        {

            var pins = await _problemPinProvider.GetModeratingPins();

            if (pins is null)
                return NotFound("Problem pins is not found");

            return Ok(pins);
        }

        [HttpGet("/api/pin/accepted")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProblemPinViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAllAcceptedPins()
        {

            var pins = await _problemPinProvider.GetAcceptedPins();

            if (pins is null)
                return NotFound("Problem pins is not found");

            return Ok(pins);
        }

        [HttpGet("/api/pin/solving")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProblemPinViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAllSolvingPins()
        {

            var pins = await _problemPinProvider.GetSolvingPins();

            if (pins is null)
                return NotFound("Problem pins is not found");

            return Ok(pins);
        }

        [HttpGet("/api/pin/solved")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProblemPinViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAllSolvedPins()
        {

            var pins = await _problemPinProvider.GetSolvedPins();

            if (pins is null)
                return NotFound("Problem pins is not found");

            return Ok(pins);
        }

        //Moderate Pin
        [HttpPatch("/accept/{id}")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AcceptPinFromUser(Guid id, [FromBody] PinStateViewModel parameter)
        {
            var moderator = await _authenticationService.GetAdminByHeaders(
                Request.Headers[HeaderNames.Authorization].ToArray());
            var problemPin = await _problemPinProvider.GetById(id);

            problemPin.State = parameter.State;
            problemPin.AcceptedModeratorAnswer = parameter.Answer;
            problemPin.AcceptedModeratorId = moderator.Id;
            await _problemPinProvider.Edit(problemPin);
            return Ok();
        }

        [HttpPatch("/moderate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ModerateReportFromBrigade(Guid id, [FromBody] PinStateViewModel parameter)
        {
            var moderator = await _authenticationService.GetAdminByHeaders(
                Request.Headers[HeaderNames.Authorization].ToArray());
            var problemPin = await _problemPinProvider.GetById(id);
            if (problemPin.State == PinState.Solved)
                return Conflict("Эта проблемная точка уже была решена");
            if (problemPin.State == PinState.Denied)
                return Conflict($"Эта проблемная точка была отклонена по причине: {problemPin.AcceptedModeratorAnswer}");
            if (problemPin.State != PinState.Solving)
                return Conflict("Работы над этой проблемной точкое не начали проводить");
            problemPin.State = parameter.State;
            problemPin.ModeratedModeratorAnswer = parameter.Answer;
            problemPin.ModeratedModeratorId = moderator.Id;
            await _problemPinProvider.Edit(problemPin);
            return Ok();
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Edit(Guid id, PutProblemPinViewModel problemPinViewModel)
        {
            var problemPin = await _problemPinProvider.GetById(id);
            if (problemPin is null)
                return NotFound("Problem pin is not found");

            problemPin.Title = problemPinViewModel.Title;
            problemPin.Description = problemPinViewModel.Description;
            problemPin.Latitude = problemPinViewModel.Latitude;
            problemPin.Longitude = problemPinViewModel.Longitude;
            problemPin.Images = await _imageProvider.AddToDatabaseAndGetAll(problemPinViewModel.Images);
            problemPin.Tags = await _tagProvider.GetAllAndCreateIfNotContains(problemPinViewModel.Tags);
            await _problemPinProvider.Edit(problemPin);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {

            var problemPin = await _problemPinProvider.GetById(id);
            if (problemPin is null)
                return NotFound("Problem pin is not found");
            await _problemPinProvider.Remove(problemPin);
            return Ok();
        }
        
    }
}