using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class Regions
    {
        public Regions()
        {
            Airports = new HashSet<Airports>();
        }

        public int RegionId { get; set; }
        public string RegionCode { get; set; }
        public int CountryId { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Airports> Airports { get; set; }
    }
}
