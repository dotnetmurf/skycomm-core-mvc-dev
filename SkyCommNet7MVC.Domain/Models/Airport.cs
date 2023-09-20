using System.ComponentModel.DataAnnotations;

namespace SkyCommNet7MVC.Domain.Models;

public partial class Airport
{
    [Display(Name = "ID")]
    public int AirportId { get; set; }

    [Display(Name = "IATA Code")]
    public string AirportIatacode { get; set; } = null!;

    [Display(Name = "Identifier")]
    public string AirportIdentifier { get; set; } = null!;

    [Display(Name = "Type ID")]
    public int AirportTypeId { get; set; }

    [Display(Name = "Name")]
    public string AirportName { get; set; } = null!;

    [Display(Name = "Latitude")]
    public double AirportLatitudeDegrees { get; set; }

    [Display(Name = "Longitude")]
    public double AirportLongitudeDegrees { get; set; }

    [Display(Name = "Elevation")]
    public int? AirportElevationFeet { get; set; }

    [Display(Name = "Region ID")]
    public int RegionId { get; set; }

    [Display(Name = "Municipality")]
    public string? AirportMunicipality { get; set; }

    [Display(Name = "Scheduled Service")]
    public bool? AirportScheduledService { get; set; }

    [Display(Name = "GPS Code")]
    public string? AirportGpscode { get; set; }

    [Display(Name = "Local Code")]
    public string? AirportLocalCode { get; set; }

    [Display(Name = "Home URL")]
    public string? AirportHomeLink { get; set; }

    [Display(Name = "Wikipedia URL")]
    public string? AirportWikipediaLink { get; set; }

    [Display(Name = "SkyComm Ops Level ID")]
    public int SkyCommOpsLevelId { get; set; }

    [Display(Name = "Ssma Time Stamp")]
    public byte[] SsmaTimeStamp { get; set; } = null!;

    [Display(Name = "Airport Type")]
    public virtual AirportType AirportType { get; set; } = null!;

    [Display(Name = "Orders")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [Display(Name = "Region")]
    public virtual Region Region { get; set; } = null!;

    [Display(Name = "SkyComm Ops Level")]
    public virtual SkyCommOpsLevel SkyCommOpsLevel { get; set; } = null!;

    [Display(Name = "SkyComm Projects")]
    public virtual ICollection<SkyCommProject> SkyCommProjects { get; set; } = new List<SkyCommProject>();

    [Display(Name = "Units")]
    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
