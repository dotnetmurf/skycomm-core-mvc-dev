using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IOfficeService
    {
        public IOrderedQueryable<Office> GetAllOffices();
        public Office GetOfficeById(int id);
        public IQueryable<Office> GetOfficesWhere(Expression<Func<Office, bool>> filter);
        public IEnumerable<Office> GetOfficesSelectList();
        public bool OfficeExists(int id);
    }
}
