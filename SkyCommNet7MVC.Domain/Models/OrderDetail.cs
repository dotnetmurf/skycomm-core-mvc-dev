namespace SkyCommNet7MVC.Domain.Models;

public partial class OrderDetail
{
    public int OrderDetailsId { get; set; }

    public int OrderId { get; set; }

    public int UnitModelId { get; set; }

    public decimal ModelPrice { get; set; }

    public short ModelQuantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual UnitModel UnitModel { get; set; } = null!;
}
