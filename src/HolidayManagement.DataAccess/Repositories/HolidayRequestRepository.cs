using HolidayManagement.Core.Models;
using HolidayManagement.DataAccess.Contexts;
using HolidayManagement.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HolidayManagement.DataAccess.Repositories
{
    public class HolidayRequestRepository : IHolidayRequestRepository
    {
        private HolidayContext context;

        public HolidayRequestRepository(HolidayContext context)
            => this.context = context;

        public async Task<IEnumerable<HolidayRequest>> GetHolidayRequestsAsync(
            Expression<Func<HolidayRequest, bool>> predicate)
            => await context.HolidayRequests
            .Where(predicate)
            .ToListAsync();

        public Task<HolidayRequest> GetHolidayRequestAsync(int id)
            => context.HolidayRequests
            .Include(e => e.Employee)
            .SingleOrDefaultAsync(e => e.Id == id);

        public Task UpdateHolidayRequestAsync(HolidayRequest request)
        {
            context.HolidayRequests.Update(request);
            return context.SaveChangesAsync();
        }

        public async Task<HolidayRequest> CreateHolidayRequestAsync(HolidayRequest request)
        {
            context.HolidayRequests.Add(request);
            await context.SaveChangesAsync();
            return request;
        }
    }
}
