namespace Domain.Entities;

public sealed class Topic
{
    public int Id { get; set; }
    public string? PhotoKey { get; set; }
    public Topic? MainTopic { get; set; }
}