using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class ModelModTypes
    {
        public ModelModTypes()
        {
            UnitModels = new HashSet<UnitModels>();
        }

        public int ModelModTypeId { get; set; }
        public string ModelModType { get; set; }

        public virtual ICollection<UnitModels> UnitModels { get; set; }
    }
}
