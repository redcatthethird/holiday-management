using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HolidayManagement.Core.Models;

namespace HolidayManagement.DataAccess.Interfaces
{
    public interface IHolidayRequestRepository
    {
        Task<IEnumerable<HolidayRequest>> GetHolidayRequestsAsync(
            Expression<Func<HolidayRequest, bool>> predicate
        );

        Task<HolidayRequest> GetHolidayRequestAsync(int id);

        Task UpdateHolidayRequestAsync(HolidayRequest request);

        Task<HolidayRequest> CreateHolidayRequestAsync(HolidayRequest request);
    }
}