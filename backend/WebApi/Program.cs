var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddValidators();
services.AddEndpoints();

services.AddApplicationDbContext();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints()
    .AddValidationFilter();

app.Run();