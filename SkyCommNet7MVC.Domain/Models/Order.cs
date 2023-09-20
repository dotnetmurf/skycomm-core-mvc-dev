namespace SkyCommNet7MVC.Domain.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int AirportId { get; set; }

    public int EmployeeId { get; set; }

    public string? OrderNotes { get; set; }

    public byte[] SsmaTimeStamp { get; set; } = null!;

    public virtual Airport Airport { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
