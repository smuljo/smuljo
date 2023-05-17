using Application.Interfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Endpoints.Registration;

public sealed class RegistrationEndpoint : IEndpoint<RegistrationRequest, IResult>
{
    private readonly IPasswordHasher _passwordHasher;

    public RegistrationEndpoint(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    private ApplicationDbContext ApplicationDbContext { get; set; } = default!;

    public async Task<IResult> HandleAsync(RegistrationRequest request)
    {
        if (await ApplicationDbContext.Users.AnyAsync(u => u.UserName == request.UserName))
        {
            return Results.Conflict($"User with login {request.UserName} already exists");
        }

        var hashedPassword = _passwordHasher.Hash(request.Password);

        var user = new User
        {
            UserName = request.UserName,
            PasswordHash = hashedPassword
        };

        ApplicationDbContext.Users.Add(user);

        await ApplicationDbContext.SaveChangesAsync();

        return Results.Ok("User successfully created!");
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/registration",
            async ([Validate] RegistrationRequest request, ApplicationDbContext applicationDbContext) =>
            {
                ApplicationDbContext = applicationDbContext;
                return await HandleAsync(request);
            }).AllowAnonymous();
    }
}