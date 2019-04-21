using HolidayManagement.Core.Models;
using HolidayManagement.DataAccess.Contexts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayManagement.DataAccess
{
    public static class DbInitialiser
    {
        public static async Task Seed(HolidayContext context)
        {
            await context.Database.EnsureCreatedAsync();

            var baseDate = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);
            DateTimeOffset afterDays(double d)
                => baseDate + TimeSpan.FromDays(d);

            if (context.Employees.Any())
                return;

            await context.Employees.AddRangeAsync(
                new Employee
                {
                    Name = "Standard Employee 1",
                    Role = EmployeeRole.Standard,
                    HolidayRequests = new Collection<HolidayRequest>
                    {
                        new HolidayRequest
                        {
                            StartDate = afterDays(3),
                            EndDate = afterDays(4),
                            Status = HolidayRequestStatus.Pending
                        },
                        new HolidayRequest
                        {
                            StartDate = afterDays(13),
                            EndDate = afterDays(14),
                            Status = HolidayRequestStatus.Approved
                        },
                    }
                },
                new Employee
                {
                    Name = "Standard Employee 2",
                    Role = EmployeeRole.Standard,
                    HolidayRequests = new Collection<HolidayRequest>
                    {
                        new HolidayRequest
                        {
                            StartDate = afterDays(43),
                            EndDate = afterDays(45),
                            Status = HolidayRequestStatus.Pending
                        },
                        new HolidayRequest
                        {
                            StartDate = afterDays(16),
                            EndDate = afterDays(21),
                            Status = HolidayRequestStatus.Refused
                        },
                    }
                },
                new Employee
                {
                    Name = "Standard Admin 1",
                    Role = EmployeeRole.Admin,
                    HolidayRequests = new Collection<HolidayRequest>
                    {
                        new HolidayRequest
                        {
                            StartDate = afterDays(3),
                            EndDate = afterDays(4),
                            Status = HolidayRequestStatus.Pending
                        },
                        new HolidayRequest
                        {
                            StartDate = afterDays(13),
                            EndDate = afterDays(14),
                            Status = HolidayRequestStatus.Pending
                        },
                    }
                },
                new Employee
                {
                    Name = "Standard Employee 1",
                    Role = EmployeeRole.Standard,
                    HolidayRequests = new Collection<HolidayRequest>
                    {
                        new HolidayRequest
                        {
                            StartDate = afterDays(1),
                            EndDate = afterDays(30),
                            Status = HolidayRequestStatus.Refused
                        },
                        new HolidayRequest
                        {
                            StartDate = afterDays(10),
                            EndDate = afterDays(10.5),
                            Status = HolidayRequestStatus.Approved
                        },
                    }
                });

            await context.SaveChangesAsync();
        }
    }
}
