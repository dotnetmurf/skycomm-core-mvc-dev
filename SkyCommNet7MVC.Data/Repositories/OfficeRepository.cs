using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class OfficeRepository : Repository<Office>, IOfficeRepository
    {
        private readonly SkyCommDbContext _context;

        public OfficeRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<Office> GetAllOffices()
        {
            var allOffices =
                from office in GetAll().
                    Include(o => o.Country)
                orderby office.OfficeName
                select office;

            return allOffices;
        }

        public Office GetOfficeById(int id)
        {
            return GetById(id);
        }

        public IQueryable<Office> GetOfficesWhere(Expression<Func<Office, bool>> filter)
        {
            var officesWhere =
                from office in Where(filter).
                    Include(o => o.Country)
                orderby office.OfficeName
                select office;

            return officesWhere;
        }

        public IEnumerable<Office> GetOfficesSelectList()
        {
            return GetAllOffices().AsEnumerable();
        }

        public bool OfficeExists(int id)
        {
            return (_context.Offices?.Any(o => o.OfficeId == id)).GetValueOrDefault();
        }
    }
}
