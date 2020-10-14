using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class ModelCategories
    {
        public ModelCategories()
        {
            UnitModels = new HashSet<UnitModels>();
        }

        public int ModelCategoryId { get; set; }
        public string ModelCategory { get; set; }
        public int ModelTypeId { get; set; }

        public virtual ModelTypes ModelType { get; set; }
        public virtual ICollection<UnitModels> UnitModels { get; set; }
    }
}
