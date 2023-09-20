using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IJobTitleRepository : IRepository<JobTitle>
    {
        public IOrderedQueryable<JobTitle> GetAllJobTitles();
        public JobTitle GetJobTitleById(int id);
        public IQueryable<JobTitle> GetJobTitlesWhere(Expression<Func<JobTitle, bool>> filter);
        public IEnumerable<JobTitle> GetJobTitlesSelectList();
    }
}
