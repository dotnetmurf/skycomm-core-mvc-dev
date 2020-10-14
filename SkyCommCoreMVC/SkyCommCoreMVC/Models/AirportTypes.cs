using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class AirportTypes
    {
        public AirportTypes()
        {
            Airports = new HashSet<Airports>();
        }

        public int AirportTypeId { get; set; }
        public string AirportType { get; set; }

        public virtual ICollection<Airports> Airports { get; set; }
    }
}
