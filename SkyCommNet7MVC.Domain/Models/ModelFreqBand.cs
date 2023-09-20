namespace SkyCommNet7MVC.Domain.Models;

public partial class ModelFreqBand
{
    public int ModelFreqBandId { get; set; }

    public string ModelFreqBand1 { get; set; } = null!;

    public virtual ICollection<UnitModel> UnitModels { get; set; } = new List<UnitModel>();
}
