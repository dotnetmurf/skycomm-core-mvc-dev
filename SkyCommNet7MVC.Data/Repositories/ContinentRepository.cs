using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class ContinentRepository : Repository<Continent>, IContinentRepository
    {
        private readonly SkyCommDbContext _context;

        public ContinentRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<Continent> GetAllContinents()
        {
            var allContinents =
                from continent in GetAll()
                orderby continent.Continent1
                select continent;

            return allContinents;
        }

        public Continent GetContinentById(int id)
        {
            return GetById(id);
        }
    }
}
