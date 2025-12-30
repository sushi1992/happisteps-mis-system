namespace HappiSteps.Application.Auth;

public sealed record MicrosoftUser(
    string Email,
    string DisplayName,
    string MicrosoftObjectId
);
