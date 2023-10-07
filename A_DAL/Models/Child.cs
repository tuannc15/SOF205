using System;
using System.Collections.Generic;

namespace A_DAL.Models
{
    public partial class Child
    {
        public int ChildId { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public bool Sex { get; set; }
        public int ParentId { get; set; }

        public virtual Parent Parent { get; set; } = null!;
    }
}
