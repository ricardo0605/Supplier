using Business.Models.Validations.Documents;
using FluentValidation;

namespace Business.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("The field  {PropertyName} is required.")
                .Length(2, 100)
                .WithMessage("The field {PropertyName} needs to have between {MinLength} and {MaxLength} characters.");

            When(s => s.SupplierType == SupplierType.Person, () =>
            {
                RuleFor(s => s.DocumentNumber.Length).Equal(CPFValidation.CPFLenght)
                    .WithMessage("This documento needs to have {ComparisonValue} characters e has {PropertyValue}.");
                RuleFor(s => CPFValidation.Validate(s.DocumentNumber)).Equal(true)
                    .WithMessage("This document number is not valid.");
            });

            When(s => s.SupplierType == SupplierType.LegalPerson, () =>
            {
                RuleFor(s => s.DocumentNumber.Length).Equal(CNPJValidation.CNPJLength)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(s => CNPJValidation.Validate(s.DocumentNumber)).Equal(true)
                    .WithMessage("This document number is not valid.");
            });
        }
    }
}