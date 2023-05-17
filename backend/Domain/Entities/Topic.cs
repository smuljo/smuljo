namespace Domain.Entities;

public sealed class Topic
{
    public int Id { get; set; }
    public Topic? MainTopic { get; set; }
    public required string Title { get; set; }
    public ICollection<Comment> Comments { get; set; } = default!;
}