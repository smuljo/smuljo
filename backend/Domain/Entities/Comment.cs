namespace Domain.Entities;

public sealed class Comment
{
    public int Id { get; set; }
    public User User { get; set; } = default!;
    public Topic Topic { get; set; } = default!;
    public required string Text { get; set; }
}