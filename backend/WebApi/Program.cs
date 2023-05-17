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

services.AddCors(options => options.AddPolicy("frontend", b => b
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()));

var app = builder.Build();

app.UseCors("frontend");
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints().RequireCors("frontend")
    .AddValidationFilter();

app.Run();