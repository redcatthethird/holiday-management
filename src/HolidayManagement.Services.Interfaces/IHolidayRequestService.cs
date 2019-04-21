using HolidayManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayManagement.Services.Interfaces
{
    public interface IHolidayRequestService
    {
        Task<HolidayRequest> GetHolidayRequestAsync(int id);

        Task<IEnumerable<HolidayRequest>> GetPendingRequestsAsync();

        Task<IEnumerable<HolidayRequest>> GetEmployeeRequestsAsync(int employeeId);

        Task ReviewHolidayRequestAsync(int id, HolidayRequestStatus status);

        Task AddHolidayRequestAsync(
            int employeeId,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            string comments);
    }
}
