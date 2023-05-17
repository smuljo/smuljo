using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Endpoints.ReadUniversityTopics;

public sealed class ReadUniversityTopicsEndpoint : IEndpoint<PaginatedRequest, IResult>
{    
    private ApplicationDbContext ApplicationDbContext { get; set; } = default!;

    public async Task<IResult> HandleAsync(PaginatedRequest request)
    {
        var topics = await ApplicationDbContext.Topics
            .Where(t => t.MainTopic == null)
            .OrderBy(t => t.Title)
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(t => new ReadUniversityTopicsItem
            {
                Id = t.Id,
                Title = t.Title
            })
            .ToListAsync();

        return PaginatedResponse.CreateResult(topics, request.ItemsCount);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/topics/university",
            async ([AsParameters, Validate] PaginatedRequest request, ApplicationDbContext applicationDbContext) =>
            {
                ApplicationDbContext = applicationDbContext;
                return await HandleAsync(request);
            });
    }
}