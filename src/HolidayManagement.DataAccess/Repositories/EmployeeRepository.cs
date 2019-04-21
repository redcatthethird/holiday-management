using HolidayManagement.Core.Models;
using HolidayManagement.DataAccess.Contexts;
using HolidayManagement.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayManagement.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private HolidayContext context;

        public EmployeeRepository(HolidayContext context)
            => this.context = context;

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
            => await context.Employees.ToListAsync();

        public Task<Employee> GetEmployeeWithHolidaysAsync(int id)
            => context.Employees
            .Include(e => e.HolidayRequests)
            .SingleOrDefaultAsync(e => e.Id == id);
    }
}