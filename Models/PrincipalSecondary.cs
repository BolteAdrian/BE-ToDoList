using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class PrincipalSecondary
    {
        public string Title { get; set; } = null!;
        public string Objective { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Checked { get; set; }
        public int Priority { get; set; }
        public string StTitle { get; set; } = null!;
        public string StObjective { get; set; } = null!;
        public string StDescription { get; set; } = null!;
        public DateTime StStartDate { get; set; }
        public DateTime StEndDate { get; set; }
        public bool StChecked { get; set; }
        public int StPriority { get; set; }
    }
}
