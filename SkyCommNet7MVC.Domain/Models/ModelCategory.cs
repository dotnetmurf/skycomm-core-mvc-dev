namespace SkyCommNet7MVC.Domain.Models;

public partial class ModelCategory
{
    public int ModelCategoryId { get; set; }

    public string ModelCategory1 { get; set; } = null!;

    public int ModelTypeId { get; set; }

    public virtual ModelType ModelType { get; set; } = null!;

    public virtual ICollection<UnitModel> UnitModels { get; set; } = new List<UnitModel>();
}
