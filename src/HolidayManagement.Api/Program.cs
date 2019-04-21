using HolidayManagement.DataAccess;
using HolidayManagement.DataAccess.Contexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HolidayManagement.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            // inserting seeding logic here based on
            // https://stackoverflow.com/questions/46222692/asp-net-core-2-seed-database
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider
                    .GetRequiredService<HolidayContext>();
                await DbInitialiser.Seed(context);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(ConfigureLogging)
                .UseStartup<Startup>();

        public static void ConfigureLogging(WebHostBuilderContext host, ILoggingBuilder builder)
        {
            builder.AddConfiguration(host.Configuration.GetSection("Logging"));
            builder.AddConsole();
            builder.AddDebug();
            builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
        }
    }
}
