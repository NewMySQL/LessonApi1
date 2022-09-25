using System;
using System.Collections.Generic;

namespace Lesson1Api.Entities
{
    public partial class Worktype
    {
        public Worktype()
        {
            Workrequests = new HashSet<Workrequest>();
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Workrequest> Workrequests { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
