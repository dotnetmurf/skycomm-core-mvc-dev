using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Offices = new HashSet<Offices>();
            Regions = new HashSet<Regions>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int ContinentId { get; set; }
        public string WikipediaLink { get; set; }
        public byte[] SsmaTimeStamp { get; set; }

        public virtual Continents Continent { get; set; }
        public virtual ICollection<Offices> Offices { get; set; }
        public virtual ICollection<Regions> Regions { get; set; }
    }
}
