using System;

namespace HolidayManagement.Core.Models
{
    public class HolidayRequest
    {
        public int Id { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public HolidayRequestStatus Status { get; set; }

        public int EmployeeId { get; set; }
        
        public Employee Employee { get; set; }
    }
}