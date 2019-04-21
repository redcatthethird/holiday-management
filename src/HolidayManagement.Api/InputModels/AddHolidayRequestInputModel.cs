using System;

namespace HolidayManagement.Api.InputModels
{
    public class AddHolidayRequestInputModel
    {
        public int EmployeeId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string Comments { get; set; }
    }
}
