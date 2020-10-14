using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int AirportId { get; set; }
        public int EmployeeId { get; set; }
        public string OrderNotes { get; set; }
        public byte[] SsmaTimeStamp { get; set; }

        public virtual Airports Airport { get; set; }
        public virtual Employees Employee { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
