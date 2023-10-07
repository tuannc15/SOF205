using System;
using System.Collections.Generic;

namespace A_DAL.Models
{
    public partial class Parent
    {
        public Parent()
        {
            Children = new HashSet<Child>();
        }

        public int ParentId { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Sex { get; set; }

        public virtual ICollection<Child> Children { get; set; }
    }
}
