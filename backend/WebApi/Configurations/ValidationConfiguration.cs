using FluentValidation;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ValidationConfiguration
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        return services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);
    }

    public static IEndpointConventionBuilder AddValidationFilter(this IEndpointConventionBuilder builder)
    {
        return builder.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);
    }
}