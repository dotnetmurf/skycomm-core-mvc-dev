using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class ModelManufacturers
    {
        public ModelManufacturers()
        {
            UnitModels = new HashSet<UnitModels>();
        }

        public int ModelManufacturerId { get; set; }
        public string ModelManufacturerName { get; set; }
        public string ModelManufacturerLink { get; set; }

        public virtual ICollection<UnitModels> UnitModels { get; set; }
    }
}
