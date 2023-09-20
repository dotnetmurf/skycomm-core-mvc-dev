namespace SkyCommNet7MVC.Domain.Models;

public partial class SkyCommService
{
    public int ServiceId { get; set; }

    public DateTime ServiceStartDate { get; set; }

    public DateTime? ServiceEndDate { get; set; }

    public int UnitId { get; set; }

    public string ContactName { get; set; } = null!;

    public string ContactPhone { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public int EmployeeId { get; set; }

    public string Problem { get; set; } = null!;

    public string? Repair { get; set; }

    public string? ServiceNotes { get; set; }

    public byte[] SsmaTimeStamp { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
