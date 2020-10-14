using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int UnitModelId { get; set; }
        public decimal ModelPrice { get; set; }
        public short ModelQuantity { get; set; }

        public virtual Orders Order { get; set; }
        public virtual UnitModels UnitModels { get; set; }
    }
}
