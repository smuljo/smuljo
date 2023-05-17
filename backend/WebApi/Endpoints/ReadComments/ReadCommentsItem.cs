namespace WebApi.Endpoints.ReadComments;

public sealed class ReadCommentsItem
{
    public required string UserName { get; set; }
    public required string Text { get; set; }
    public required IEnumerable<string> Materials { get; set; }
}