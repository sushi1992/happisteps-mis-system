using System.IdentityModel.Tokens.Jwt;
using HappiSteps.Application.Auth;
using HappiSteps.Application.Common.Interfaces;

namespace HappiSteps.Infrastructure.Auth;

public sealed class MicrosoftTokenValidator : IMicrosoftTokenValidator
{
    private readonly HttpClient _http;

    public MicrosoftTokenValidator(HttpClient http)
    {
        _http = http;
    }

    public async Task<MicrosoftUser> ValidateCode(string idToken)
    {
        // Decode ID token (issued by Microsoft to SPA)
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(idToken);

        var email =
            token.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value
            ?? token.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

        if (email is null)
            throw new InvalidOperationException("Microsoft token missing email");

        var name =
            token.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? email;

        var oid =
            token.Claims.FirstOrDefault(c => c.Type == "oid")?.Value
            ?? Guid.NewGuid().ToString();

        return new MicrosoftUser(
            Email: email,
            DisplayName: name,
            MicrosoftObjectId: oid);
    }
}
