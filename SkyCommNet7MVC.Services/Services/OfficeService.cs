using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;

        public OfficeService(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public IOrderedQueryable<Office> GetAllOffices()
        {
            return _officeRepository.GetAllOffices();
        }

        public Office GetOfficeById(int id)
        {
            return _officeRepository.GetOfficeById(id);
        }

        public IQueryable<Office> GetOfficesWhere(Expression<Func<Office, bool>> filter)
        {
            return _officeRepository.GetOfficesWhere(filter);
        }

        public IEnumerable<Office> GetOfficesSelectList()
        {
            return GetAllOffices().AsEnumerable();
        }

        public bool OfficeExists(int id)
        {
            return _officeRepository.OfficeExists(id);
        }
    }
}
