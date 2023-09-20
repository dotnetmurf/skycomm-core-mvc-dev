using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IJobTitleService
    {
        public IOrderedQueryable<JobTitle> GetAllJobTitles();
        public JobTitle GetJobTitleById(int id);
        public IQueryable<JobTitle> GetJobTitlesWhere(Expression<Func<JobTitle, bool>> filter);
        public IEnumerable<JobTitle> GetJobTitlesSelectList();
    }
}
