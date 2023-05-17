using Application.Interfaces;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class JwtGeneratorConfiguration
{
    public static IServiceCollection AddJwtGenerator(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        return services.AddSingleton<IJwtGenerator, JwtGenerator>();
    }
}