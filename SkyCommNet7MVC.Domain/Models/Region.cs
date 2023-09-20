namespace SkyCommNet7MVC.Domain.Models;

public partial class Region
{
    public int RegionId { get; set; }

    public string RegionCode { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual ICollection<Airport> Airports { get; set; } = new List<Airport>();

    public virtual Country Country { get; set; } = null!;
}
