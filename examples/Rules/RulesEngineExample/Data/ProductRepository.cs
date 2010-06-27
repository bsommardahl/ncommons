using System.Collections.Generic;
using System.Linq;
using RulesEngineExample.Domain;

namespace RulesEngineExample.Data
{
    public class ProductRepository : IProductRepository
    {
        static int _primaryKey = 0;
        static readonly IList<Product> _products = new List<Product>();

        static ProductRepository()
        {
            _products.Add(new Product { Id = _primaryKey++, Description = "Red Cap", Price = 5.00d });
            _products.Add(new Product { Id = _primaryKey++, Description = "Blue Shirt", Price = 10.00d });
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.ToList();
        }

        public void Save(Product product)
        {
            if (!_products.Contains(product))
            {
                product.Id = _primaryKey++;
                _products.Add(product);
            }
        }

        public void Delete(int id)
        {
            var product = _products.Where(p => p.Id == id).SingleOrDefault();
            _products.Remove(product);
        }
    }
}