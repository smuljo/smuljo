using Application.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Endpoints.Login;

public sealed class LoginEndpoint : IEndpoint<LoginRequest, IResult>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public LoginEndpoint(
        IJwtGenerator jwtGenerator,
        IPasswordHasher passwordHasher)
    {
        _jwtGenerator = jwtGenerator;
        _passwordHasher = passwordHasher;
    }

    private ApplicationDbContext ApplicationDbContext { get; set; } = default!;

    public async Task<IResult> HandleAsync(LoginRequest request)
    {
        var user = await ApplicationDbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

        if (user is null)
        {
            return Results.UnprocessableEntity($"User with {request.UserName} not found.");
        }

        var passwordHash = _passwordHasher.Hash(request.Password);

        if (passwordHash != user.PasswordHash)
        {
            return Results.Problem("Login / password wrong.");
        }

        var token = _jwtGenerator.Generate(user);
        var response = new LoginResponse
        {
            AccessToken = token
        };

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/login",
                async ([Validate] LoginRequest request, ApplicationDbContext applicationDbContext) =>
                {
                    ApplicationDbContext = applicationDbContext;
                    return await HandleAsync(request);
                })
            .AllowAnonymous();
    }
}