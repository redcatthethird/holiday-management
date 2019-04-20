using System.Collections.Generic;

namespace HolidayManagement.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EmployeeRole Role { get; set; }

        public ICollection<HolidayRequest> HolidayRequests { get; set; }
    }
}