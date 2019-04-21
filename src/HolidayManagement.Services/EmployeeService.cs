using HolidayManagement.Core.Models;
using HolidayManagement.DataAccess.Interfaces;
using HolidayManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repo;

        public EmployeeService(IEmployeeRepository repo)
            => this.repo = repo;

        public Task<Employee> GetEmployeeAsync(int id)
            => repo.GetEmployeeWithHolidaysAsync(id);

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
            => await repo.GetEmployeesAsync();
    }
}
