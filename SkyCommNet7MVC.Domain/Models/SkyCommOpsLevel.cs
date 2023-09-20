namespace SkyCommNet7MVC.Domain.Models;

public partial class SkyCommOpsLevel
{
    public int SkyCommOpsLevelId { get; set; }

    public string SkyCommOpsLevel1 { get; set; } = null!;

    public virtual ICollection<Airport> Airports { get; set; } = new List<Airport>();
}
