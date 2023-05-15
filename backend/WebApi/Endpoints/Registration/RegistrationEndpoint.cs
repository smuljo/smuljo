using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

public sealed class RegistrationEndpoint : IEndpoint<RegistrationRequest, IResult>
{
    private ApplicationDbContext ApplicationDbContext { get; set; } = default!;

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/registration",
            async([AsParameters, Validate] RegistrationRequest request, ApplicationDbContext applicationDbContext) =>
            {
                ApplicationDbContext = applicationDbContext;
                return await HandleAsync(request);
            });
    }

    public async Task<IResult> HandleAsync(RegistrationRequest request)
    {
        if (await UserExists(request.UserName))
        {
            return Results.Conflict("User with this login already exists");
        }
        
        var hashedPassword = GetPasswordsHash(request.Password);

        var user = new User
        {
            UserName = request.UserName,
            PasswordHash = request.Password
        };

        ApplicationDbContext.Users.Add(user);

        await ApplicationDbContext.SaveChangesAsync();

        return Results.Ok("User successfully created!");

        async Task<bool> UserExists(string username) => await ApplicationDbContext.Users.AnyAsync(u => u.UserName == username);
    }

    private string GetPasswordsHash(string password)
    {
        SHA256 sha256Hash = SHA256.Create();

        byte[] bytes = Encoding.UTF8.GetBytes(password);

        byte[] hash = sha256Hash.ComputeHash(bytes);

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            builder.Append(hash[i].ToString("x2"));
        }
        return builder.ToString();
    }
}

