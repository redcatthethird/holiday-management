using HolidayManagement.DataAccess.Contexts;
using HolidayManagement.DataAccess.Interfaces;
using HolidayManagement.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HolidayManagement.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
            => services
            .AddTransient<IEmployeeRepository, EmployeeRepository>()
            .AddTransient<IHolidayRequestRepository, HolidayRequestRepository>();

        public static IServiceCollection AddContext(
            this IServiceCollection services,
            string dbPath)
            => services.AddDbContext<HolidayContext>(options
                => options.UseSqlite($"Data Source={dbPath}"));
    }
}
