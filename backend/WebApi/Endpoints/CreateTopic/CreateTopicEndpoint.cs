using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Endpoints.CreateTopic;

public sealed class CreateTopicEndpoint : IEndpoint<CreateTopicRequest, IResult>
{
    private ApplicationDbContext ApplicationDbContext { get; set; } = default!;

    public async Task<IResult> HandleAsync(CreateTopicRequest request)
    {
        var existedTopic = await ApplicationDbContext.Topics
            .Where(t => t.MainTopic == null)
            .FirstOrDefaultAsync(t => t.Title == request.Title);

        if (existedTopic is not null)
        {
            return Results.Conflict($"Topic with title {request.Title} already exist.");
        }

        Topic topic;
        if (request.MainTopicId.HasValue)
        {
            var mainTopicId = request.MainTopicId.Value;
            var mainTopic = await ApplicationDbContext.Topics
                .Include(t => t.MainTopic)
                .FirstOrDefaultAsync(t => t.Id == mainTopicId);

            if (mainTopic is null)
            {
                return Results.UnprocessableEntity($"Not found topic with id {mainTopicId}");
            }

            if (mainTopic.MainTopic is not null)
            {
                return Results.BadRequest($"Main topic with id {mainTopicId} already has main topic.");
            }

            var topicWithSameNameExists = await ApplicationDbContext.Topics
                .Where(t => t.MainTopic!.Id == mainTopicId)
                .AnyAsync(t => t.Title == request.Title);

            if (topicWithSameNameExists)
            {
                return Results.Conflict($"Subject topic in main topic {mainTopic.Title} already exist.");
            }

            topic = new Topic
            {
                Title = request.Title,
                MainTopic = mainTopic
            };
        }
        else
        {
            topic = new Topic
            {
                Title = request.Title
            };
        }

        ApplicationDbContext.Topics.Add(topic);
        await ApplicationDbContext.SaveChangesAsync();
        return Results.Ok();
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/topics",
            async ([Validate] CreateTopicRequest request, ApplicationDbContext applicationDbContext) =>
            {
                ApplicationDbContext = applicationDbContext;
                return await HandleAsync(request);
            });
    }
}