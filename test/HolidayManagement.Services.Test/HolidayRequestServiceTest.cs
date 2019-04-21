using HolidayManagement.Core.Models;
using HolidayManagement.DataAccess.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PredicateExpression = System.Linq.Expressions.Expression<
    System.Func<
        HolidayManagement.Core.Models.HolidayRequest,
        bool>>;

namespace HolidayManagement.Services.Test
{
    public class HolidayRequestServiceTest : IClassFixture<HolidayRequestServiceFixture>
    {
        private readonly HolidayRequestServiceFixture fixture;

        public HolidayRequestServiceTest(HolidayRequestServiceFixture fixture)
            => this.fixture = fixture;

        [Fact]
        public async Task GetHolidayRequest_ShouldGetCorrect()
        {
            var requestedHolidayId = 1;
            var expectedHolidayStartDate = HolidayRequestServiceFixture.baseDate;
            var expectedEmployeeId = 11;
            var service = new HolidayRequestService(fixture.Object);

            var result = await service.GetHolidayRequestAsync(requestedHolidayId);

            Assert.Equal(requestedHolidayId, result.Id);
            Assert.Equal(expectedHolidayStartDate, result.StartDate);
            Assert.Equal(expectedEmployeeId, result.Employee.Id);
            Assert.Equal(expectedEmployeeId, result.EmployeeId);
        }

        [Fact]
        public async Task GetPendingRequests_ShouldRespectPredicate()
        {
            var expectedHolidayRequestIds = new[] { 1, 4 };
            var service = new HolidayRequestService(fixture.Object);

            var result = await service.GetPendingRequestsAsync();

            Assert.Equal(expectedHolidayRequestIds.Length, result.Count());
            Assert.Equal(expectedHolidayRequestIds, result.Select(e => e.Id));
            Assert.All(result, r => Assert.Equal(HolidayRequestStatus.Pending, r.Status));
        }

        [Fact]
        public async Task GetEmployeeRequests_ShouldRespectPredicate()
        {
            var requestedEmployeeId = 11;
            var expectedHolidayRequestIds = new[] { 1, 2, 4 };
            var service = new HolidayRequestService(fixture.Object);

            var result = await service.GetEmployeeRequestsAsync(requestedEmployeeId);

            Assert.Equal(expectedHolidayRequestIds.Length, result.Count());
            Assert.Equal(expectedHolidayRequestIds, result.Select(e => e.Id));
            Assert.All(result, r => Assert.Equal(requestedEmployeeId, r.EmployeeId));
        }

        [Theory]
        [InlineData(HolidayRequestStatus.Approved)]
        [InlineData(HolidayRequestStatus.Refused)]
        public async Task ReviewHolidayRequest_ShouldUpdate(HolidayRequestStatus status)
        {
            // arrange
            var requestedHolidayRequestId = 1;
            var service = new HolidayRequestService(fixture.Object);

            // act
            await service.ReviewHolidayRequestAsync(
                requestedHolidayRequestId,
                status);

            // assert
            fixture.Verify(
                s => s.UpdateHolidayRequestAsync(
                    It.Is<HolidayRequest>(
                        r => r.Status == status)));

            // cleanup
            var requestedHoliday = await fixture.Object
                .GetHolidayRequestAsync(requestedHolidayRequestId);
            requestedHoliday.Status = HolidayRequestStatus.Pending;
        }

        [Fact]
        public async Task SubmitHolidayRequest_ShouldCreate()
        {
            // arrange
            var created = new
            {
                StartDate = HolidayRequestServiceFixture.baseDate + TimeSpan.FromDays(2),
                EndDate = HolidayRequestServiceFixture.baseDate + TimeSpan.FromDays(3),
                Comments = "some comments",
                EmployeeId = 11
            };
            var service = new HolidayRequestService(fixture.Object);

            await service.AddHolidayRequestAsync(
                created.EmployeeId,
                created.StartDate,
                created.EndDate,
                created.Comments);

            fixture.Verify(
                r => r.CreateHolidayRequestAsync(
                    It.Is<HolidayRequest>(
                        hr => hr.EmployeeId == created.EmployeeId &&
                            hr.StartDate == created.StartDate &&
                            hr.EndDate == created.EndDate &&
                            hr.Comments == created.Comments)));
        }

        [Fact]
        public async Task SubmitHolidayRequest_WithoutComments_ShouldCreate()
        {
            // arrange
            var created = new
            {
                StartDate = HolidayRequestServiceFixture.baseDate + TimeSpan.FromDays(2),
                EndDate = HolidayRequestServiceFixture.baseDate + TimeSpan.FromDays(3),
                Comments = null as string,
                EmployeeId = 11
            };
            var service = new HolidayRequestService(fixture.Object);

            await service.AddHolidayRequestAsync(
                created.EmployeeId,
                created.StartDate,
                created.EndDate,
                created.Comments);

            fixture.Verify(
                r => r.CreateHolidayRequestAsync(
                    It.Is<HolidayRequest>(
                        hr => hr.EmployeeId == created.EmployeeId &&
                            hr.StartDate == created.StartDate &&
                            hr.EndDate == created.EndDate &&
                            hr.Comments == created.Comments)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task SubmitHolidayRequest_WithInvalidEmployeeId_ShouldNotCreate(int employeeId)
        {
            // arrange
            var created = new
            {
                StartDate = HolidayRequestServiceFixture.baseDate + TimeSpan.FromDays(2),
                EndDate = HolidayRequestServiceFixture.baseDate + TimeSpan.FromDays(3),
                Comments = "some comments",
                EmployeeId = employeeId
            };
            var service = new HolidayRequestService(fixture.Object);

            await Assert.ThrowsAnyAsync<Exception>(() => service.AddHolidayRequestAsync(
                created.EmployeeId,
                created.StartDate,
                created.EndDate,
                created.Comments));
        }

        [Fact]
        public async Task SubmitHolidayRequest_WithReversedDates_ShouldNotCreate()
        {
            // arrange
            var created = new
            {
                StartDate = HolidayRequestServiceFixture.baseDate + TimeSpan.FromDays(3),
                EndDate = HolidayRequestServiceFixture.baseDate + TimeSpan.FromDays(2),
                Comments = "some comments",
                EmployeeId = 11
            };
            var service = new HolidayRequestService(fixture.Object);

            await Assert.ThrowsAnyAsync<Exception>(() => service.AddHolidayRequestAsync(
                created.EmployeeId,
                created.StartDate,
                created.EndDate,
                created.Comments));
        }
    }

    public class HolidayRequestServiceFixture : Mock<IHolidayRequestRepository>
    {
        public static readonly DateTimeOffset baseDate = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

        private static readonly Employee employee1 = new Employee
        {
            Id = 11,
            Name = "Test Employee 11",
            Role = EmployeeRole.Standard
        };

        private static readonly Employee employee2 = new Employee
        {
            Id = 22,
            Name = "Test Employee 22",
            Role = EmployeeRole.Admin
        };

        private readonly IEnumerable<HolidayRequest> holidayRequests = new List<HolidayRequest>
        {
            new HolidayRequest
            {
                Id = 1,
                StartDate = baseDate,
                EndDate = baseDate + TimeSpan.FromDays(1),
                Status = HolidayRequestStatus.Pending,
                EmployeeId = employee1.Id,
                Employee = employee1
            },
            new HolidayRequest
            {
                Id = 2,
                StartDate = baseDate + TimeSpan.FromDays(7),
                EndDate = baseDate + TimeSpan.FromDays(7.5),
                Status = HolidayRequestStatus.Approved,
                Comments = "Some comments here",
                EmployeeId = employee1.Id,
                Employee = employee1
            },
            new HolidayRequest
            {
                Id = 3,
                StartDate = baseDate + TimeSpan.FromDays(3),
                EndDate = baseDate + TimeSpan.FromDays(6),
                Status = HolidayRequestStatus.Refused,
                EmployeeId = employee2.Id,
                Employee = employee2
            },
            new HolidayRequest
            {
                Id = 4,
                StartDate = baseDate + TimeSpan.FromDays(3),
                EndDate = baseDate + TimeSpan.FromDays(6),
                Status = HolidayRequestStatus.Pending,
                EmployeeId = employee1.Id,
                Employee = employee1
            },
        };

        public HolidayRequestServiceFixture()
        {
            Setup(r => r.GetHolidayRequestAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => holidayRequests.SingleOrDefault(r => r.Id == id));
            Setup(r => r.GetHolidayRequestsAsync(It.IsAny<PredicateExpression>()))
                .ReturnsAsync((PredicateExpression predExpr) => holidayRequests.Where(predExpr.Compile()).ToList());
            Setup(r => r.UpdateHolidayRequestAsync(It.IsAny<HolidayRequest>()))
                .Returns(Task.CompletedTask);
            Setup(r => r.CreateHolidayRequestAsync(It.IsAny<HolidayRequest>()))
                .ReturnsAsync((HolidayRequest r) => r);
        }
    }
}
