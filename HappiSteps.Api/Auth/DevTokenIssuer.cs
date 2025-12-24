using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using HappiSteps.Contracts.Auth;

namespace HappiSteps.Api.Auth;

public static class DevTokenIssuer
{
    public static string IssueToken(
        Guid userId,
        Guid organisationId)
    {
        var claims = new[]
        {
            new Claim("userId", userId.ToString()),
            new Claim("organisationId", organisationId.ToString()),
            new Claim(ClaimTypes.Role, Roles.Admin)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("super-secret-dev-key-change-me"));

        var creds = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
