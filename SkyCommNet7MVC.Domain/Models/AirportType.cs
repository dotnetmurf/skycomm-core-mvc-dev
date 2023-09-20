namespace SkyCommNet7MVC.Domain.Models;

public partial class AirportType
{
    public int AirportTypeId { get; set; }

    public string AirportType1 { get; set; } = null!;

    public virtual ICollection<Airport> Airports { get; set; } = new List<Airport>();
}
