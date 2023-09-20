namespace SkyCommNet7MVC.Domain.Models;

public partial class ModelModType
{
    public int ModelModTypeId { get; set; }

    public string ModelModType1 { get; set; } = null!;

    public virtual ICollection<UnitModel> UnitModels { get; set; } = new List<UnitModel>();
}
