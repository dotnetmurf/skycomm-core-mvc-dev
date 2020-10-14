using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class Units
    {
        public Units()
        {
            SkyCommServices = new HashSet<SkyCommServices>();
        }

        public int UnitId { get; set; }
        public int UnitModelId { get; set; }
        public string UnitScnbr { get; set; }
        public string UnitSerNbr { get; set; }
        public decimal UnitCost { get; set; }
        public int AirportId { get; set; }

        public virtual Airports Airport { get; set; }
        public virtual UnitModels UnitModels { get; set; }
        public virtual ICollection<SkyCommServices> SkyCommServices { get; set; }
    }
}
