using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class JobTitleRepository : Repository<JobTitle>, IJobTitleRepository
    {
        private readonly SkyCommDbContext _context;

        public JobTitleRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<JobTitle> GetAllJobTitles()
        {
            var allJobTitles =
                from jobTitle in GetAll().
                    Include(e => e.Department)
                orderby jobTitle.JobTitle1
                select jobTitle;

            return allJobTitles;
        }

        public JobTitle GetJobTitleById(int id)
        {
            return GetById(id);
        }

        public IQueryable<JobTitle> GetJobTitlesWhere(Expression<Func<JobTitle, bool>> filter)
        {
            var jobTitlesWhere =
                from jobTitle in Where(filter).
                    Include(e => e.Department)
                orderby jobTitle.JobTitle1
                select jobTitle;

            return jobTitlesWhere;
        }

        public IEnumerable<JobTitle> GetJobTitlesSelectList()
        {
            return GetAllJobTitles().AsEnumerable();
        }
    }
}
