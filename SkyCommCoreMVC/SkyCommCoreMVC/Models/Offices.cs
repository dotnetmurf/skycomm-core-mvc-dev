using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class Offices
    {
        public Offices()
        {
            Employees = new HashSet<Employees>();
        }

        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress1 { get; set; }
        public string OfficeAddress2 { get; set; }
        public string OfficeCity { get; set; }
        public string OfficeState { get; set; }
        public int CountryId { get; set; }
        public string OfficePostalCode { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string OfficeFaxNumber { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
