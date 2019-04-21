using HolidayManagement.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HolidayManagement.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services)
            => services
            .AddTransient<IEmployeeService, EmployeeService>()
            .AddTransient<IHolidayRequestService, HolidayRequestService>();
    }
}
