using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public IOrderedQueryable<Unit> GetAllUnits()
        {
            return _unitRepository.GetAllUnits();
        }

        public Unit GetUnitById(int id)
        {
            return _unitRepository.GetUnitById(id);
        }

        public IQueryable<Unit> GetUnitsWhere(Expression<Func<Unit, bool>> filter)
        {
            return _unitRepository.GetUnitsWhere(filter);
        }

        public IEnumerable<Unit> GetUnitsSelectList()
        {
            return _unitRepository.GetUnitsSelectList();
        }

        public bool UnitExists(int id)
        {
            return _unitRepository.UnitExists(id);
        }
    }
}
