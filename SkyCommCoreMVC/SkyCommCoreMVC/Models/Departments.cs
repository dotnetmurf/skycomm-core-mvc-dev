using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class Departments
    {
        public Departments()
        {
            JobTitles = new HashSet<JobTitles>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentAccountNbr { get; set; }

        public virtual ICollection<JobTitles> JobTitles { get; set; }
    }
}
