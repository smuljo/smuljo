using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Endpoints.ReadComments;

public sealed class ReadCommentsEndpoint : IEndpoint<PaginatedRequest<int>, IResult>
{
    private ApplicationDbContext ApplicationDbContext { get; set; } = default!;

    public async Task<IResult> HandleAsync(PaginatedRequest<int> request)
    {
        var topic = await ApplicationDbContext.Topics
            .Include(t => t.Comments)
            .ThenInclude(c => c.User)
            .Include(t => t.Comments)
            .ThenInclude(c => c.Materials)
            .FirstOrDefaultAsync(t => t.Id == request.Id);

        if (topic is null)
        {
            return Results.UnprocessableEntity($"Topic with id {request.Id} not found.");
        }

        var comments = topic.Comments
            .OrderBy(c => c.Text)
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(c => new ReadCommentsItem
            {
                UserName = c.User.UserName,
                Text = c.Text,
                Materials = c.Materials.Select(m => m.Link)
            })
            .ToList();

        return PaginatedResponse.CreateResult(comments, request.ItemsCount);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/comments",
                async ([AsParameters, Validate] PaginatedRequest<int> request,
                    ApplicationDbContext applicationDbContext) =>
                {
                    ApplicationDbContext = applicationDbContext;
                    return await HandleAsync(request);
                })
            .AllowAnonymous();
    }
}