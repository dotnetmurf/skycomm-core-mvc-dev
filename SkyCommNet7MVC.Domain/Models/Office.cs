namespace SkyCommNet7MVC.Domain.Models;

public partial class Office
{
    public int OfficeId { get; set; }

    public string OfficeName { get; set; } = null!;

    public string OfficeAddress1 { get; set; } = null!;

    public string OfficeAddress2 { get; set; } = null!;

    public string OfficeCity { get; set; } = null!;

    public string? OfficeState { get; set; }

    public int CountryId { get; set; }

    public string? OfficePostalCode { get; set; }

    public string OfficePhoneNumber { get; set; } = null!;

    public string OfficeFaxNumber { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
