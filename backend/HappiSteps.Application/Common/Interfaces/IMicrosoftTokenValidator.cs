using HappiSteps.Application.Auth;

namespace HappiSteps.Application.Common.Interfaces;

public interface IMicrosoftTokenValidator
{
    Task<MicrosoftUser> ValidateCode(string code);
}
