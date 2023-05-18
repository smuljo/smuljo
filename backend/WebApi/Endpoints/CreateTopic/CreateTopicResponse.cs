namespace WebApi.Endpoints.CreateTopic;

public sealed class CreateTopicResponse
{
    public required int Id { get; set; }
    public required string Title { get; set; }
}