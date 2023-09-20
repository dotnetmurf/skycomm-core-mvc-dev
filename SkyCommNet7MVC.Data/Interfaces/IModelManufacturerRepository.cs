﻿using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IModelManufacturerRepository
    {
        public IOrderedQueryable<ModelManufacturer> GetAllModelManufacturers();
        public ModelManufacturer GetModelManufacturerById(int id);
        public IQueryable<ModelManufacturer> GetModelManufacturersWhere(Expression<Func<ModelManufacturer, bool>> filter);
        public IEnumerable<ModelManufacturer> GetModelManufacturersSelectList();
        public bool ModelManufacturerExists(int id);
    }
}
