using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class SecondaryTask
    {
        public int Id { get; set; }
        public int? PrincipalTaskId { get; set; }
        public string Title { get; set; } = null!;
        public string Objective { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Checked { get; set; }
        public int Priority { get; set; }

        public virtual PrincipalTask? PrincipalTask { get; set; }
    }
}
