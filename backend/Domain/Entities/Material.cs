namespace Domain.Entities;

public sealed class Material
{
    public int Id { get; set; }
    public Comment Comment { get; set; } = default!;
    public required MaterialType MaterialType { get; set; }
    public required string Link { get; set; }
}

public enum MaterialType
{
    Image,
    Video,
    Document
}