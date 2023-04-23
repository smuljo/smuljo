using FluentValidation;

namespace WebApi.Endpoints.Random;

public sealed class RandomRequestValidator : AbstractValidator<RandomRequest>
{
    public RandomRequestValidator()
    {
        RuleFor(request => request.Max)
            .GreaterThan(request => request.Min)
            .WithMessage("'{PropertyName}' must be greater than '{ComparisonProperty}'.");
    }
}