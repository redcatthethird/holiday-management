using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HolidayManagement.Api.InputModels;
using HolidayManagement.Core.Models;
using HolidayManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IHolidayRequestService service;

        public RequestsController(IHolidayRequestService service)
            => this.service = service;

        [HttpGet]
        public Task<IEnumerable<HolidayRequest>> GetPending()
            => service.GetPendingRequestsAsync();

        [HttpGet("{id}")]
        public Task<HolidayRequest> Get(int id)
            => service.GetHolidayRequestAsync(id);

        [HttpGet("employee/{id}")]
        public Task<IEnumerable<HolidayRequest>> GetForEmployee(int id)
            => service.GetEmployeeRequestsAsync(id);
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddHolidayRequestInputModel model)
        {
            if (model.EmployeeId <= 0 || model.EndDate < model.StartDate)
                return BadRequest();

            await service.AddHolidayRequestAsync(
                model.EmployeeId,
                model.StartDate,
                model.EndDate,
                model.Comments);
            return Ok();
        }

        [HttpPost("review/{id}")]
        public async Task<IActionResult> Review(int id, [FromBody] string value)
        {
            if (!Enum.TryParse<HolidayRequestStatus>(value, false, out var status))
                return BadRequest();

            await service.ReviewHolidayRequestAsync(id, status);
            return Ok();
        }
    }
}
