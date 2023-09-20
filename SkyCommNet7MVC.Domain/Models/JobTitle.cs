namespace SkyCommNet7MVC.Domain.Models;

public partial class JobTitle
{
    public int JobTitleId { get; set; }

    public string? JobTitle1 { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
