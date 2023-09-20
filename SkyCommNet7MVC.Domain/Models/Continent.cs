namespace SkyCommNet7MVC.Domain.Models;

public partial class Continent
{
    public int ContinentId { get; set; }

    public string Continent1 { get; set; } = null!;

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
