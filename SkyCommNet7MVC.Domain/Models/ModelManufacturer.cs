namespace SkyCommNet7MVC.Domain.Models;

public partial class ModelManufacturer
{
    public int ModelManufacturerId { get; set; }

    public string ModelManufacturerName { get; set; } = null!;

    public string? ModelManufacturerLink { get; set; }

    public string ModelManufacturerImage { get; set; }

    public virtual ICollection<UnitModel> UnitModels { get; set; } = new List<UnitModel>();
}
