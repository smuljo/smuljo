using FluentValidation;

namespace WebApi.Endpoints.CreateComment;

public sealed class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
{
    public CreateCommentRequestValidator()
    {
        RuleFor(request => request.Text)
            .NotEmpty()
            .Length(1, 500);

        RuleForEach(request => request.MaterialLinks)
            .NotEmpty()
            .Length(3, 100);

        RuleForEach(request => request.MaterialLinks)
            .NotEmpty()
            .Length(3, 50);
    }
}