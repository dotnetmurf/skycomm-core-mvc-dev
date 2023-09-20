using SkyCommNet7MVC.Domain.SelectListModels;

namespace SkyCommNet7MVC.Services.SelectLists
{
    public class SearchEmployeeFieldsSelectList
    {
        public static List<SearchEmployeeFields> GetSearchEmployeeFieldsSelectList()
        {
            return new List<SearchEmployeeFields>
            {
                new SearchEmployeeFields() { Id = "Both", EmployeeFieldName = "First or Last Name" },
                new SearchEmployeeFields() { Id = "First", EmployeeFieldName = "First Name only" },
                new SearchEmployeeFields() { Id = "Last", EmployeeFieldName = "Last Name only" }
            };
        }
    }
}
