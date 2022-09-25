using System;
using System.Collections.Generic;

namespace Lesson1.Entities
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeOnly StartWorkTime { get; set; }
        public TimeOnly FinishWorkTime { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
