using NCommons.Rules.Mapping;
using RulesEngineExample.Domain;
using RulesEngineExample.Models;

namespace RulesEngineExample.Validators.Configuration
{
    public class ProductInputAssociationConfiguration : AssociationConfiguration<ProductInput>
    {
        public ProductInputAssociationConfiguration()
        {
            ConfigureAssociationsFor<Product>(x =>
                {
                    x.For(output => output.Id).Use(input => input.Id);
                    x.For(output => output.Description).Use(input => input.Description);
                    x.For(output => output.Price).Use(input => input.Price);
                });
        }
    }
}