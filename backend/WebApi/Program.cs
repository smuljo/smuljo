var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddValidators();
services.AddEndpoints();

var app = builder.Build();

app.MapEndpoints()
    .AddValidationFilter();

app.Run();