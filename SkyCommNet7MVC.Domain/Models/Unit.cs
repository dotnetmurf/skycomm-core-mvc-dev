namespace SkyCommNet7MVC.Domain.Models;

public partial class Unit
{
    public int UnitId { get; set; }

    public int UnitModelId { get; set; }

    public string UnitScnbr { get; set; } = null!;

    public string UnitSerNbr { get; set; } = null!;

    public decimal UnitCost { get; set; }

    public int AirportId { get; set; }

    public virtual Airport Airport { get; set; } = null!;

    public virtual ICollection<SkyCommService> SkyCommServices { get; set; } = new List<SkyCommService>();

    public virtual UnitModel UnitModel { get; set; } = null!;
}
