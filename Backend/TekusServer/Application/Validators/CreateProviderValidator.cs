using Application.Commands.Providers.Create;
using FluentValidation;

namespace Application.Validators
{
    public class CreateProviderValidator : AbstractValidator<CreateProviderCommand>
    {
        public CreateProviderValidator()
        {
            RuleFor(x => x.Nit)
                .NotEmpty().WithMessage("NIT is required")
                .MaximumLength(50);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be a valid address");
        }
    }
}
