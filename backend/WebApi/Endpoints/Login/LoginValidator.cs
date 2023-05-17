using FluentValidation;

namespace WebApi.Endpoints.Login;

public sealed class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(request => request.UserName)
            .NotEmpty()
            .Length(3, 25);
        
        RuleFor(request => request.Password)
            .NotEmpty()
            .Length(6, 50);
    }
}