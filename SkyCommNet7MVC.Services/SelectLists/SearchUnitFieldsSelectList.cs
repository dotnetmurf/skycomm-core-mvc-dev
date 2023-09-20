using SkyCommNet7MVC.Domain.SelectListModels;

namespace SkyCommNet7MVC.Services.SelectLists
{
    public class SearchUnitFieldsSelectList
    {
        public static List<SearchUnitFields> GetSearchUnitFieldsSelectList()
        {
            return new List<SearchUnitFields>
            {
                new SearchUnitFields() { Id = "Both", UnitFieldName = "SkyComm Number or Serial Number" },
                new SearchUnitFields() { Id = "SCNbr", UnitFieldName = "SkyComm Number only" },
                new SearchUnitFields() { Id = "SerNbr", UnitFieldName = "Serial Number only" }
            };
        }
    }
}
