using System;
using System.Collections.Generic;

namespace Lesson1.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            Schedules = new HashSet<Schedule>();
            Workrequests = new HashSet<Workrequest>();
            WorkTypes = new HashSet<Worktype>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Comment { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Workrequest> Workrequests { get; set; }

        public virtual ICollection<Worktype> WorkTypes { get; set; }
    }
}
