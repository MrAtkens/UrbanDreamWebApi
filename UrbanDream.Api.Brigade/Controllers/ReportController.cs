using BazarJok.DataAccess.Models;
using BazarJok.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BazarJok.Contracts.ViewModels;
using BazarJok.Contracts.ViewModels.Reports;
using BazarJok.DataAccess.Models.Pins;
using BazarJok.DataAccess.Models.Reports;
using BazarJok.DataAccess.Providers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace BazarJok.Api.Vendor.Controllers
{
    [EnableCors("Policy")]
    [Route("api/report/")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly ReportProvider _reportProvider;
        private readonly ReportImageProvider _imageProvider;
        private readonly ProblemPinProvider _problemPinProvider;
        private readonly BrigadeAuthenticationService _brigadeAuthenticationService;

        public ReportController(ReportProvider reportProvider, ReportImageProvider imageProvider, ProblemPinProvider problemPinProvider,
            BrigadeAuthenticationService brigadeAuthenticationService)
        {
            _reportProvider = reportProvider;
            _imageProvider = imageProvider;
            _problemPinProvider = problemPinProvider;
            _brigadeAuthenticationService = brigadeAuthenticationService;
        }

        [HttpGet("brigade/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReportViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByBrigadeId(Guid id)
        {
            var reports = await _reportProvider.GetByBrigadeId(id);

            if (reports is null)
                return NotFound("Reports are not found");

            return Ok(reports);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReportViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAll()
        {
            var brigade = await _brigadeAuthenticationService.GetBrigadeByHeaders(Request.Headers[HeaderNames.Authorization]
                .ToArray());

            var reports = await _reportProvider.GetByBrigadeId(brigade.Id);

            if (reports is null)
                return NotFound("Products are not found");

            return Ok(reports);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(PostReportViewModel reportViewModel)
        {
            var brigade = await _brigadeAuthenticationService.GetBrigadeByHeaders(Request.Headers[HeaderNames.Authorization]
                .ToArray());
            var pins = new List<ProblemPin>();
            var pin = await _problemPinProvider.GetById(reportViewModel.Pin);
            var report = new Report()
            {
                Name = reportViewModel.Name,
                Description = reportViewModel.Description,
                Images = await _imageProvider.AddToDatabaseAndGetAll(reportViewModel.Images),
                BrigadeId = brigade.Id,
                WorkStartTime = reportViewModel.WorkStartTime,
                Pins = pins,
                State = ReportState.Waiting
            };
            pins.Add(pin);
            await _reportProvider.Add(report);
            pin.Report = report;
            await _problemPinProvider.Edit(pin);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Edit(Guid id, PutReportViewModel putReportView)
        {
            var brigade = await _brigadeAuthenticationService.GetBrigadeByHeaders(Request.Headers[HeaderNames.Authorization]
                .ToArray());
            var report = await _reportProvider.GetById(id);
            if (report is null)
                return NotFound("Product is not found");
            if (report.BrigadeId != brigade.Id)
                return Forbid();
            report.Name = putReportView.Name;
            report.Description = putReportView.Description;
            report.Images = await _imageProvider.AddToDatabaseAndGetAll(putReportView.Images);
            await _reportProvider.Edit(report);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var brigade = await _brigadeAuthenticationService.GetBrigadeByHeaders(Request.Headers[HeaderNames.Authorization]
                .ToArray());
            var report = await _reportProvider.GetById(id);
            if (report is null)
                return NotFound("Report is not found");
            var pin = await _problemPinProvider.GetById(report.Pins[0].Id);
            if (pin is null)
                return NotFound("Pin is not found");
            
            if (report.BrigadeId != brigade.Id)
                return Forbid();
            await _reportProvider.Remove(report);
            pin.Report = null;
            await _problemPinProvider.Edit(pin);
            return Ok();
        }

        
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetCount()
        {
            var brigade = await _brigadeAuthenticationService.GetBrigadeByHeaders(Request.Headers[HeaderNames.Authorization]
                .ToArray());
            return Ok(await _reportProvider.GetCountOfReport(brigade.Id));
        }
    }
}