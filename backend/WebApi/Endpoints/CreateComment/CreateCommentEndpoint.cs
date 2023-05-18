using System.Security.Claims;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Endpoints.CreateComment;

public sealed class CreateCommentEndpoint : IEndpoint<CreateCommentRequest, IResult>
{
    private ApplicationDbContext ApplicationDbContext { get; set; } = default!;
    private HttpContext HttpContext { get; set; } = default!;

    public async Task<IResult> HandleAsync(CreateCommentRequest request)
    {
        var userId = HttpContext.User.GetUserId();

        var topic = await ApplicationDbContext.Topics
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == request.TopicId);

        if (topic is null)
        {
            return Results.UnprocessableEntity($"Topic with id {request.TopicId} not found.");
        }

        var user = await ApplicationDbContext.Users.FirstAsync(u => u.Id == userId);

        var comment = new Comment
        {
            Text = request.Text,
            User = user,
            Materials = request.MaterialLinks
                .Select(link => new Material
                {
                    Link = link
                })
                .ToArray()
        };

        topic.Comments.Add(comment);
        await ApplicationDbContext.SaveChangesAsync();

        var response = new CreateCommentResponse
        {
            UserName = user.UserName,
            Text = request.Text
        };

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/comments",
            async ([Validate] CreateCommentRequest request, ApplicationDbContext applicationDbContext,
                HttpContext httpContext) =>
            {
                ApplicationDbContext = applicationDbContext;
                HttpContext = httpContext;
                return await HandleAsync(request);
            });
    }
}