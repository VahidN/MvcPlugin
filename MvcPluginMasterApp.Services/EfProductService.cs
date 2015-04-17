using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MvcPluginMasterApp.DomainClasses;
using MvcPluginMasterApp.PluginsBase;
using MvcPluginMasterApp.Services.Contracts;

namespace MvcPluginMasterApp.Services
{
    public class EfProductService : IProductService
    {
        IUnitOfWork _uow;
        readonly IDbSet<Product> _products;
        public EfProductService(IUnitOfWork uow)
        {
            _uow = uow;
            _products = _uow.Set<Product>();
        }

        public void AddNewProduct(Product product)
        {
            _products.Add(product);
        }

        public IList<Product> GetAllProducts()
        {
            return _products.Include(x => x.Category).ToList();
        }
    }
}