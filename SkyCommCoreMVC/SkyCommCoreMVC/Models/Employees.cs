using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Orders = new HashSet<Orders>();
            SkyCommProjects = new HashSet<SkyCommProjects>();
            SkyCommServices = new HashSet<SkyCommServices>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int JobTitleId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int OfficeId { get; set; }
        public string ImageFileName { get; set; }

        public virtual JobTitles JobTitle { get; set; }
        public virtual Offices Office { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<SkyCommProjects> SkyCommProjects { get; set; }
        public virtual ICollection<SkyCommServices> SkyCommServices { get; set; }
    }
}
