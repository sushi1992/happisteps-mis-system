using HappiSteps.Application.Children.CreateChild;
using HappiSteps.Application.Children.GetChildById;
using HappiSteps.Api.Auth;
using HappiSteps.Infrastructure.Persistence;
using HappiSteps.Domain.Common;
using HappiSteps.Infrastructure.Persistence.Repositories;
using HappiSteps.Infrastructure.Audit;
using HappiSteps.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.EntityFrameworkCore;
using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Application.Admissions.ConfirmAdmission;
using HappiSteps.Application.Admissions.LeaveAdmission;
using HappiSteps.Application.Children.ArchiveChild;
using HappiSteps.ReadModel.Admissions.GetOnRollRegister;
using HappiSteps.ReadModel.Admissions.GetAdmissionHistoryForChild;
using HappiSteps.ReadModel.Children.GetChildrenForOrganisation;
using HappiSteps.ReadModel.Children.GetChildDetails;
using HappiSteps.ReadModel.Dashboard.GetOrganisationDashboardStats;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Services
// =======================

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("super-secret-dev-key-change-me")),
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<HappiStepsDbContext>(options =>
{
    options.UseSqlite("Data Source=happisteps.db");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IOrganisationContext, OrganisationContext>();

builder.Services.AddScoped<IChildRepository, ChildRepository>();
builder.Services.AddScoped<IAdmissionRepository, AdmissionRepository>();
builder.Services.AddScoped<IAuditLogger, AuditLogger>();
builder.Services.AddScoped<IUserContext, UserContext>();

builder.Services.AddScoped<CreateChildHandler>();
builder.Services.AddScoped<GetChildByIdHandler>();
builder.Services.AddScoped<ConfirmAdmissionHandler>();
builder.Services.AddScoped<LeaveAdmissionHandler>();
builder.Services.AddScoped<GetOnRollRegisterHandler>();
builder.Services.AddScoped<GetAdmissionHistoryForChildHandler>();
builder.Services.AddScoped<GetChildrenForOrganisationHandler>();
builder.Services.AddScoped<GetChildDetailsHandler>();
builder.Services.AddScoped<ArchiveChildHandler>();
builder.Services.AddScoped<GetOrganisationDashboardStatsHandler>();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => "HappiSteps API is running 🚀");
app.Run();
