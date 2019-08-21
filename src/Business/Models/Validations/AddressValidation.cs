using FluentValidation;

namespace Business.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(2, 200).WithMessage("{PropertyName} needs to have between {MinLength} and {MaxLength} characters.");

            RuleFor(c => c.Neighborhood)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(2, 100).WithMessage("{PropertyName} needs to have between {MinLength} and {MaxLength} characters.");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(8).WithMessage("{PropertyName} needs to have between {MinLength} and {MaxLength} characters.");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(2, 100).WithMessage("{PropertyName} needs to have between {MinLength} and {MaxLength} characters.");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(2, 50).WithMessage("{PropertyName} needs to have between {MinLength} and {MaxLength} characters.");

            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(1, 50).WithMessage("{PropertyName} needs to have between {MinLength} and {MaxLength} characters.");
        }
    }
}