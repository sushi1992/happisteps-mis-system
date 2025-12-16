using HappiSteps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Services
// =======================

builder.Services.AddDbContext<HappiStepsDbContext>(options =>
{
    options.UseSqlite("Data Source=happisteps.db");
});

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
