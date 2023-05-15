using FluentValidation;

public sealed class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(request => request.UserName)
            .NotEmpty()
            .Length(1, 30);

        RuleFor(request => request.Password)
            .NotEmpty()
            .Length(6, 100);
    }
}

