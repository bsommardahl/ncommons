using System.Collections.Generic;

namespace RulesEngineExample.Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        void Save(Product product);
        void Delete(int id);
    }
}