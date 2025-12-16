using HappiSteps.Infrastructure.Persistence;
using HappiSteps.Domain.Children;
using HappiSteps.Domain.Common;
using HappiSteps.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Services
// =======================

builder.Services.AddDbContext<HappiStepsDbContext>(options =>
{
    options.UseSqlite("Data Source=happisteps.db");
});

builder.Services.AddScoped<IChildRepository, ChildRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

// Built-in OpenAPI (.NET 9)
builder.Services.AddOpenApi();

var app = builder.Build();

// =======================
// Middleware
// =======================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => "HappiSteps API is running 🚀");
app.Run();
