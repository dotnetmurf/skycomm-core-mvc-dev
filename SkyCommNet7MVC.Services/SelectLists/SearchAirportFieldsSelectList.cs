using SkyCommNet7MVC.Domain.SelectListModels;

namespace SkyCommNet7MVC.Services.SelectLists
{
    public class SearchAirportFieldsSelectList
    {
        public static List<SearchAirportFields> GetSearchAirportFieldsSelectList()
        {
            return new List<SearchAirportFields>
            {
                new SearchAirportFields() { Id = "Both", AirportFieldName = "IATA Code or Name" },
                new SearchAirportFields() { Id = "Code", AirportFieldName = "IATA Code only" },
                new SearchAirportFields() { Id = "Name", AirportFieldName = "Name only" }
            };
        }
    }
}
