using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using HappiSteps.Contracts.Auth;
using HappiSteps.Application.Common.Interfaces;

namespace HappiSteps.Api.Auth;

public sealed class DevTokenIssuer : ITokenIssuer
{
    private readonly string _signingKey;

    public DevTokenIssuer(IConfiguration config)
    {
        _signingKey = config["Jwt:SigningKey"]
            ?? throw new InvalidOperationException("JWT signing key not configured");
    }

    public string IssueToken(
        Guid userId,
        Guid organisationId,
        IReadOnlyCollection<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim("organisationId", organisationId.ToString())
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_signingKey));

        var creds = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
