using FluentValidation;

namespace Business.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(2, 200).WithMessage("{PropertyName} needs to have between {MinLength} and {MaxLength} characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(2, 1000).WithMessage("{PropertyName} needs to have between {MinLength} and {MaxLength} characters.");

            RuleFor(c => c.Value)
                .GreaterThan(0).WithMessage("{PropertyName} have to be greater than {ComparisonValue}.");
        }
    }
}