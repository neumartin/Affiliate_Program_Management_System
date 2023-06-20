using APMS.Domain.Entities;
using APMS.Managers.Common;

namespace APMS.Managers.Interfaces;

public interface IApiKeyManager : IManager<ApiKey>
{
    Task<bool> ValidateAsync(string key, string secret);
}