using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class JobTitles
    {
        public JobTitles()
        {
            Employees = new HashSet<Employees>();
        }

        public int JobTitleId { get; set; }
        public string JobTitle { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Departments Department { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
