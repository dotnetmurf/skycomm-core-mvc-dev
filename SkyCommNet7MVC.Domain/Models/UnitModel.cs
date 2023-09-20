namespace SkyCommNet7MVC.Domain.Models;

public partial class UnitModel
{
    public int UnitModelId { get; set; }

    public string ModelCode { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public string ModelImage { get; set; } = null!;

    public decimal ModelMsrp { get; set; }

    public int ModelManufacturerId { get; set; }

    public int ModelCategoryId { get; set; }

    public int ModelFreqBandId { get; set; }

    public int ModelModTypeId { get; set; }

    public virtual ModelCategory ModelCategory { get; set; } = null!;

    public virtual ModelFreqBand ModelFreqBand { get; set; } = null!;

    public virtual ModelManufacturer ModelManufacturer { get; set; } = null!;

    public virtual ModelModType ModelModType { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
