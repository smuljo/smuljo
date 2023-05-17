using FluentValidation;

namespace WebApi.Endpoints.CreateTopic;

public sealed class CreateTopicRequestValidator : AbstractValidator<CreateTopicRequest>
{
    public CreateTopicRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty()
            .Length(3, 30);
    }
}