namespace SkyCommNet7MVC.Domain.Models;

public partial class ModelType
{
    public int ModelTypeId { get; set; }

    public string ModelType1 { get; set; } = null!;

    public virtual ICollection<ModelCategory> ModelCategories { get; set; } = new List<ModelCategory>();
}
