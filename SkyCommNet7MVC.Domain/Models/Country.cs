namespace SkyCommNet7MVC.Domain.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public int ContinentId { get; set; }

    public string? WikipediaLink { get; set; }

    public byte[] SsmaTimeStamp { get; set; } = null!;

    public virtual Continent Continent { get; set; } = null!;

    public virtual ICollection<Office> Offices { get; set; } = new List<Office>();

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
}
