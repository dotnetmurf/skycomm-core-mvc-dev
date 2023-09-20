using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IOfficeRepository : IRepository<Office>
    {
        public IOrderedQueryable<Office> GetAllOffices();
        public Office GetOfficeById(int id);
        public IQueryable<Office> GetOfficesWhere(Expression<Func<Office, bool>> filter);
        public IEnumerable<Office> GetOfficesSelectList();
        public bool OfficeExists(int id);
    }
}
