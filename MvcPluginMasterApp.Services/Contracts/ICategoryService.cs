using System.Collections.Generic;
using MvcPluginMasterApp.DomainClasses;

namespace MvcPluginMasterApp.Services.Contracts
{
    public interface ICategoryService
    {
        void AddNewCategory(Category category);
        IList<Category> GetAllCategories();
    }
}
