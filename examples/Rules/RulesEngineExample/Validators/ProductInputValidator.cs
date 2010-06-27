using FluentValidation;
using RulesEngineExample.Domain;

namespace RulesEngineExample.Validators
{
    public class ProductInputValidator : AbstractValidator<Product>
    {
        public ProductInputValidator()
        {
            RuleFor(product => product.Description).NotEmpty().WithMessage("Description can not be empty.");
            RuleFor(product => product.Price).NotEqual(0);
        }
    }
}