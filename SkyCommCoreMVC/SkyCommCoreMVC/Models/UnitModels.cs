using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class UnitModels
    {
        public UnitModels()
        {
            OrderDetails = new HashSet<OrderDetails>();
            Units = new HashSet<Units>();
        }

        public int UnitModelId { get; set; }
        public string ModelCode { get; set; }
        public string ModelName { get; set; }
        public string ModelImage { get; set; }
        public decimal ModelMsrp { get; set; }
        public int ModelManufacturerId { get; set; }
        public int ModelCategoryId { get; set; }
        public int ModelFreqBandId { get; set; }
        public int ModelModTypeId { get; set; }

        public virtual ModelCategories ModelCategory { get; set; }
        public virtual ModelFreqBands ModelFreqBand { get; set; }
        public virtual ModelManufacturers ModelManufacturer { get; set; }
        public virtual ModelModTypes ModelModType { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Units> Units { get; set; }
    }
}
