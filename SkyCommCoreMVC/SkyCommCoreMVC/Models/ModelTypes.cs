using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class ModelTypes
    {
        public ModelTypes()
        {
            ModelCategories = new HashSet<ModelCategories>();
        }

        public int ModelTypeId { get; set; }
        public string ModelType { get; set; }

        public virtual ICollection<ModelCategories> ModelCategories { get; set; }
    }
}
