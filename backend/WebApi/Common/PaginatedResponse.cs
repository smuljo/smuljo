namespace WebApi.Common;

public static class PaginatedResponse
{
    public static IResult CreateResult<TItem>(IList<TItem> items, int itemsCount)
    {
        if (!items.Any())
        {
            return Results.NoContent();
        }

        PaginatedResponse<TItem> response;
        if (items.Count <= itemsCount)
        {
            response = new PaginatedResponse<TItem>
            {
                Items = items,
                HasNext = false
            };
        }
        else
        {
            response = new PaginatedResponse<TItem>
            {
                Items = items.Except(new[] { items.Last() }),
                HasNext = true
            };
        }

        return Results.Ok(response);
    }
    
}

public sealed class PaginatedResponse<TItem>
{
    public required IEnumerable<TItem> Items { get; set; }
    public required bool HasNext { get; set; }
}