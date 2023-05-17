namespace WebApi.Endpoints.CreateTopic;

public sealed class CreateTopicRequest
{
    public required int? MainTopicId { get; set; }

    public required string Title { get; set; }
}