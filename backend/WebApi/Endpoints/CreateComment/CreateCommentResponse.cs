namespace WebApi.Endpoints.CreateComment;

public sealed class CreateCommentResponse
{
    public required string Text { get; set; }
    public required string UserName { get; set; }
}