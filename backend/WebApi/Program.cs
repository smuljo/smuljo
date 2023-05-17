using Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddEndpointsApiExplorer();
services.AddSwagger();

services.AddValidators();
services.AddEndpoints();
services.AddPasswordHasher();

var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()!;
services.AddJwtGenerator(configuration);
services.AddJwtAuth(jwtSettings);

services.AddApplicationDbContext();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints()
    .AddValidationFilter();

app.Run();