using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class SkyCommOpsLevels
    {
        public SkyCommOpsLevels()
        {
            Airports = new HashSet<Airports>();
        }

        public int SkyCommOpsLevelId { get; set; }
        public string SkyCommOpsLevel { get; set; }

        public virtual ICollection<Airports> Airports { get; set; }
    }
}
