using System;
using System.Collections.Generic;

namespace Lesson1.Entities
{
    public partial class Client
    {
        public Client()
        {
            Workrequests = new HashSet<Workrequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public long ChatId { get; set; }

        public virtual ICollection<Workrequest> Workrequests { get; set; }
    }
}
