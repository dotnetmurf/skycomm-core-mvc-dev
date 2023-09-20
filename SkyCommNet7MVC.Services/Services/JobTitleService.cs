using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository _jobTitleRepository;

        public JobTitleService(IJobTitleRepository jobTitleRepository)
        {
            _jobTitleRepository = jobTitleRepository;
        }

        public IOrderedQueryable<JobTitle> GetAllJobTitles()
        {
            return _jobTitleRepository.GetAllJobTitles();
        }

        public JobTitle GetJobTitleById(int id)
        {
            return _jobTitleRepository.GetJobTitleById(id);
        }

        public IQueryable<JobTitle> GetJobTitlesWhere(Expression<Func<JobTitle, bool>> filter)
        {
            return _jobTitleRepository.GetJobTitlesWhere(filter);
        }

        public IEnumerable<JobTitle> GetJobTitlesSelectList()
        {
            return GetAllJobTitles().AsEnumerable();
        }
    }
}
