namespace SkyCommNet7MVC.Domain.Models;

public partial class SkyCommProject
{
    public int ProjectId { get; set; }

    public DateTime ProjectStartDate { get; set; }

    public DateTime ProjectEndDate { get; set; }

    public int AirportId { get; set; }

    public int EmployeeId { get; set; }

    public string? ProjectNotes { get; set; }

    public byte[] SsmaTimeStamp { get; set; } = null!;

    public virtual Airport Airport { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
