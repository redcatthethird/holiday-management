using HolidayManagement.Core.Models;
using HolidayManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService service;

        public EmployeesController(IEmployeeService service)
            => this.service = service;

        [HttpGet]
        public Task<IEnumerable<Employee>> Get()
            => service.GetEmployeesAsync();

        [HttpGet("{id}")]
        public Task<Employee> Get(int id)
            => service.GetEmployeeAsync(id);
    }
}
