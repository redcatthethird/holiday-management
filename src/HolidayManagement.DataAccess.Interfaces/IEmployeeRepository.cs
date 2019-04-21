using System.Collections.Generic;
using System.Threading.Tasks;
using HolidayManagement.Core.Models;

namespace HolidayManagement.DataAccess.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeWithHolidaysAsync(int id);
    }
}