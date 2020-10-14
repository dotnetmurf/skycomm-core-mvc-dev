using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class SkyCommServices
    {
        public int SkyCommServiceId { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public int UnitId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public int EmployeeId { get; set; }
        public string Problem { get; set; }
        public string Repair { get; set; }
        public string ServiceNotes { get; set; }
        public byte[] SsmaTimeStamp { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Units Unit { get; set; }
    }
}
