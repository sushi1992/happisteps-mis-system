using HappiSteps.Application.Children.CreateChild;
using HappiSteps.Application.Children.GetChildById;
using HappiSteps.Infrastructure.Persistence;
using HappiSteps.Domain.Common;
using HappiSteps.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Application.Admissions.ConfirmAdmission;
using HappiSteps.Application.Admissions.LeaveAdmission;
using HappiSteps.ReadModel.Admissions.GetOnRollRegister;
using HappiSteps.ReadModel.Admissions.GetAdmissionHistoryForChild;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Services
// =======================

builder.Services.AddDbContext<HappiStepsDbContext>(options =>
{
    options.UseSqlite("Data Source=happisteps.db");
});

builder.Services.AddScoped<IChildRepository, ChildRepository>();
builder.Services.AddScoped<IAdmissionRepository, AdmissionRepository>();

builder.Services.AddScoped<CreateChildHandler>();
builder.Services.AddScoped<GetChildByIdHandler>();
builder.Services.AddScoped<ConfirmAdmissionHandler>();
builder.Services.AddScoped<LeaveAdmissionHandler>();
builder.Services.AddScoped<GetOnRollRegisterHandler>();
builder.Services.AddScoped<GetAdmissionHistoryForChildHandler>();

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
