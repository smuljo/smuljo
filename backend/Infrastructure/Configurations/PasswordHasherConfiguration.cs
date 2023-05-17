using System.Security.Cryptography;
using Application.Interfaces;
using Infrastructure.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class PasswordHasherConfiguration
{
    public static IServiceCollection AddPasswordHasher(this IServiceCollection services)
    {
        services.AddSingleton<HashAlgorithm>(SHA256.Create());
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        return services;
    }
}