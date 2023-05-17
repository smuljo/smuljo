using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Endpoints.ReadSubjectTopics;

public sealed class ReadSubjectTopicsEndpoint : IEndpoint<PaginatedRequest<int>, IResult>
{    
    private ApplicationDbContext ApplicationDbContext { get; set; } = default!;

    public async Task<IResult> HandleAsync(PaginatedRequest<int> request)
    {
        var topics = await ApplicationDbContext.Topics
            .Where(t => t.MainTopic!.Id == request.Id)
            .OrderBy(t => t.Title)
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(t => new ReadSubjectTopicsItem
            {
                Id = t.Id,
                Title = t.Title
            })
            .ToListAsync();

        return PaginatedResponse.CreateResult(topics, request.ItemsCount);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/topics/subject",
            async ([AsParameters, Validate] PaginatedRequest<int> request, ApplicationDbContext applicationDbContext) =>
            {
                ApplicationDbContext = applicationDbContext;
                return await HandleAsync(request);
            });
    }
}