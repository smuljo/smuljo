using FluentValidation;

namespace WebApi.Common;

public sealed class PaginatedRequestValidator : AbstractValidator<PaginatedRequest>
{
    public PaginatedRequestValidator()
    {
        RuleFor(request => request.Page)
            .GreaterThan(0);

        RuleFor(request => request.ItemsCount)
            .GreaterThan(0);
    }
}