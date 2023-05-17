namespace Domain.Entities;

public sealed class Material
{
    public int Id { get; set; }
    public Comment Comment { get; set; } = default!;
    public required string Link { get; set; }
}