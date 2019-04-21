using HolidayManagement.Core.Models;
using HolidayManagement.DataAccess.Interfaces;
using HolidayManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayManagement.Services
{
    public class HolidayRequestService : IHolidayRequestService
    {
        private readonly IHolidayRequestRepository repo;

        public HolidayRequestService(IHolidayRequestRepository repo)
            => this.repo = repo;

        public Task AddHolidayRequestAsync(int employeeId, DateTimeOffset startDate, DateTimeOffset endDate, string comments)
        {
            if (employeeId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(employeeId),
                    "Referenced employee id must be positive");
            if (endDate < startDate)
                throw new ArgumentOutOfRangeException(
                    nameof(endDate),
                    "Given end date must come after the start date");

            return repo.CreateHolidayRequestAsync(new HolidayRequest
            {
                StartDate = startDate,
                EndDate = endDate,
                EmployeeId = employeeId,
                Comments = comments,
                Status = HolidayRequestStatus.Pending
            });
        }

        public Task<IEnumerable<HolidayRequest>> GetEmployeeRequestsAsync(int employeeId)
            => repo.GetHolidayRequestsAsync(r => r.EmployeeId == employeeId);

        public Task<IEnumerable<HolidayRequest>> GetPendingRequestsAsync()
            => repo.GetHolidayRequestsAsync(r => r.Status == HolidayRequestStatus.Pending);

        public Task<HolidayRequest> GetHolidayRequestAsync(int id)
            => repo.GetHolidayRequestAsync(id);

        public async Task ReviewHolidayRequestAsync(int id, HolidayRequestStatus status)
        {
            var request = await repo.GetHolidayRequestAsync(id);
            request.Status = status;

            await repo.UpdateHolidayRequestAsync(request);
        }
    }
}
