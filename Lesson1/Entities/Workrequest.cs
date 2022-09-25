using System;
using System.Collections.Generic;

namespace Lesson1.Entities
{
    public partial class Workrequest
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime CreateTime { get; set; }
        public int EmployeeId { get; set; }
        public DateTime FullfilledDate { get; set; }
        public string Address { get; set; } = null!;
        public string? TaskDescription { get; set; }
        public ulong? IsAccepted { get; set; }
        public DateTime? BeginWorkTime { get; set; }
        public DateTime? CompleteWorkTime { get; set; }
        public int WorkTypeId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual Worktype WorkType { get; set; } = null!;
    }
}
