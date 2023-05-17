namespace WebApi.Endpoints.CreateComment;

public sealed class CreateCommentRequest
{
    public required int TopicId { get; set; }
    public required string Text { get; set; }
    public required IEnumerable<string> MaterialLinks { get; set; }
}