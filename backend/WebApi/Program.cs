var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddValidators();
services.AddEndpoints();

services.AddApplicationDbContext();

var app = builder.Build();

app.MapEndpoints()
    .AddValidationFilter();

app.Run();