using NCommons.Rules;
using RulesEngineExample.Domain;

namespace RulesEngineExample.Commands
{
    public class CreateProductCommand : ICommand<Product>
    {
        readonly IProductRepository _productRepository;

        public CreateProductCommand(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ReturnValue Execute(Product product)
        {
            _productRepository.Save(product);
            return null;
        }
    }
}