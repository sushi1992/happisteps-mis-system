using HappiSteps.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HappiSteps.Infrastructure.Auth;

public sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?
                .User
                .FindFirst("userId");

            if (claim is null)
                throw new InvalidOperationException("UserId claim missing.");

            return Guid.Parse(claim.Value);
        }
    }
}
