using Application;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationDbContextConfiguration
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
    {
        return services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((serviceProvider, builder) =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            builder.UseNpgsql(connectionString);
        });
    }
}