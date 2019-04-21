using HolidayManagement.Core.Models;
using HolidayManagement.DataAccess.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HolidayManagement.Services.Test
{
    public class EmployeeServiceTest : IClassFixture<EmployeeServiceFixture>
    {
        private readonly EmployeeServiceFixture fixture;

        public EmployeeServiceTest(EmployeeServiceFixture fixture)
            => this.fixture = fixture;

        [Fact]
        public async Task GetEmployee_ShouldGetCorrect()
        {
            var requestedEmployeeId = 1;
            var expectedEmployeeName = "Test employee 1";
            var expectedHolidayRequestId = 3;
            var service = new EmployeeService(fixture.Object);

            var result = await service.GetEmployeeAsync(requestedEmployeeId);

            Assert.Equal(requestedEmployeeId, result.Id);
            Assert.Equal(expectedEmployeeName, result.Name);
            Assert.Equal(expectedHolidayRequestId, result.HolidayRequests.First().Id);
        }

        [Fact]
        public async Task GetEmployees_ShouldGetAll()
        {
            var expectedEmployeeIds = new[] { 1, 2 };
            var service = new EmployeeService(fixture.Object);

            var result = await service.GetEmployeesAsync();

            Assert.Equal(expectedEmployeeIds.Length, result.Count());
            Assert.Equal(expectedEmployeeIds, result.Select(e => e.Id));
        }
    }

    public class EmployeeServiceFixture : Mock<IEmployeeRepository>
    {
        private readonly IEnumerable<Employee> employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "Test employee 1",
                Role = EmployeeRole.Standard,
                HolidayRequests = new List<HolidayRequest>
                {
                    new HolidayRequest
                    {
                        Id = 3,
                        EmployeeId = 1,
                        Comments = "Nothing to add",
                        StartDate = new DateTimeOffset(2000, 1, 1, 1, 0, 0, TimeSpan.Zero),
                        EndDate = new DateTimeOffset(2000, 1, 2, 1, 0, 0, TimeSpan.Zero)
                    }
                }
            },
            new Employee
            {
                Id = 2,
                Name = "Test employee 2",
                Role = EmployeeRole.Admin,
                HolidayRequests = new List<HolidayRequest>()
            }
        };

        public EmployeeServiceFixture()
            : base(MockBehavior.Default)
        {
            Setup(r => r.GetEmployeesAsync())
                .ReturnsAsync(employees);
            Setup(r => r.GetEmployeeWithHolidaysAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => employees.SingleOrDefault(e => e.Id == id));
        }
    }
}
