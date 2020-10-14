using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class SkyCommProjects
    {
        public int SkyCommProjectId { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public int AirportId { get; set; }
        public int EmployeeId { get; set; }
        public string ProjectNotes { get; set; }
        public byte[] SsmaTimeStamp { get; set; }

        public virtual Airports Airport { get; set; }
        public virtual Employees Employee { get; set; }
    }
}
