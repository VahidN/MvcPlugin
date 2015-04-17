using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MvcPluginMasterApp.DomainClasses;
using MvcPluginMasterApp.PluginsBase;
using MvcPluginMasterApp.Services.Contracts;

namespace MvcPluginMasterApp.Services
{
    public class EfCategoryService : ICategoryService
    {
        IUnitOfWork _uow;
        readonly IDbSet<Category> _categories;
        public EfCategoryService(IUnitOfWork uow)
        {
            _uow = uow;
            _categories = _uow.Set<Category>();
        }

        public void AddNewCategory(Category category)
        {
           _categories.Add(category);
        }

        public IList<Category> GetAllCategories()
        {
            return _categories.ToList();
        }
    }
}