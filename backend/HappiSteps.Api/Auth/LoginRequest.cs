namespace HappiSteps.Api.Controllers;

public sealed record LoginRequest(
    string Email,
    string Password
);