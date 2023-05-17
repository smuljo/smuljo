namespace WebApi.Common;

public class PaginatedRequest
{
    public required int ItemsCount { get; set; }
    public required int Page { get; set; }

    public int Skip => (Page - 1) * ItemsCount;
    public int Take => ItemsCount + 1;
}

public sealed class PaginatedRequest<TId> : PaginatedRequest
{
    public required TId Id { get; set; }
}