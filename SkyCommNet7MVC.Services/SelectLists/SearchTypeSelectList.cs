using SkyCommNet7MVC.Domain.SelectListModels;

namespace SkyCommNet7MVC.Services.SelectLists
{
    public class SearchTypeSelectList
    {
        public static List<SearchType> GetSearchTypeSelectList()
        {
            return new List<SearchType>
            {
                new SearchType() { Id = "Contains", TypeName = "Field(s) Contains" },
                new SearchType() { Id = "StartsWith", TypeName = "Field(s) Starts With" },
                new SearchType() { Id = "EndsWith", TypeName = "Field(s) Ends With" },
                new SearchType() { Id = "Exact", TypeName = "Field(s) are Exact" }
            };
        }
    }
}
