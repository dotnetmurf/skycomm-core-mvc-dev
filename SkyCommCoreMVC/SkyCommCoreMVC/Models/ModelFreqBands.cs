using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class ModelFreqBands
    {
        public ModelFreqBands()
        {
            UnitModels = new HashSet<UnitModels>();
        }

        public int ModelFreqBandId { get; set; }
        public string ModelFreqBand { get; set; }

        public virtual ICollection<UnitModels> UnitModels { get; set; }
    }
}
