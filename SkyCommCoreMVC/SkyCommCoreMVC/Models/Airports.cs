using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class Airports
    {
        public Airports()
        {
            Orders = new HashSet<Orders>();
            SkyCommProjects = new HashSet<SkyCommProjects>();
            Units = new HashSet<Units>();
        }

        public int AirportId { get; set; }
        public string AirportIatacode { get; set; }
        public string AirportIdentifier { get; set; }
        public int AirportTypeId { get; set; }
        public string AirportName { get; set; }
        public double AirportLatitudeDegrees { get; set; }
        public double AirportLongitudeDegrees { get; set; }
        public int? AirportElevationFeet { get; set; }
        public int RegionId { get; set; }
        public string AirportMunicipality { get; set; }
        public bool? AirportScheduledService { get; set; }
        public string AirportGpscode { get; set; }
        public string AirportLocalCode { get; set; }
        public string AirportHomeLink { get; set; }
        public string AirportWikipediaLink { get; set; }
        public int SkyCommOpsLevelId { get; set; }
        public byte[] SsmaTimeStamp { get; set; }

        public virtual AirportTypes AirportType { get; set; }
        public virtual Regions Region { get; set; }
        public virtual SkyCommOpsLevels SkyCommOpsLevel { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<SkyCommProjects> SkyCommProjects { get; set; }
        public virtual ICollection<Units> Units { get; set; }
    }
}
