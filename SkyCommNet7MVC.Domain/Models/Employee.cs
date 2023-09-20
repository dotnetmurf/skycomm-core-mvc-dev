namespace SkyCommNet7MVC.Domain.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public int JobTitleId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public int OfficeId { get; set; }

    public string? ImageFileName { get; set; }

    public virtual JobTitle JobTitle { get; set; } = null!;

    public virtual Office Office { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<SkyCommProject> SkyCommProjects { get; set; } = new List<SkyCommProject>();

    public virtual ICollection<SkyCommService> SkyCommServices { get; set; } = new List<SkyCommService>();
}
