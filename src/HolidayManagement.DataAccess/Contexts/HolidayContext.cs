using HolidayManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HolidayManagement.DataAccess.Contexts
{
    public class HolidayContext : DbContext
    {
        public HolidayContext(DbContextOptions<HolidayContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<HolidayRequest> HolidayRequests { get; set; }
    }
}