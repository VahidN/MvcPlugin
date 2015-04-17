using System.Collections.Generic;
using MvcPluginMasterApp.DomainClasses;

namespace MvcPluginMasterApp.Services.Contracts
{
    public interface IProductService
    {
        void AddNewProduct(Product product);
        IList<Product> GetAllProducts();
    }
}
