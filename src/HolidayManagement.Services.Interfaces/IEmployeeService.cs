using HolidayManagement.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayManagement.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeAsync(int id);

        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}
