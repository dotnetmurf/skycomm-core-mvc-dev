namespace SkyCommNet7MVC.Domain.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string DepartmentCode { get; set; } = null!;

    public string DepartmentAccountNbr { get; set; } = null!;

    public virtual ICollection<JobTitle> JobTitles { get; set; } = new List<JobTitle>();
}
