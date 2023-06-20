using APMS.DataAccess.Context;
using APMS.Domain.Entities;
using APMS.Managers.Common;
using APMS.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APMS.Managers.Managers;

public class ApiKeyManager : GenericManager<ApiKey>, IApiKeyManager
{
    public async Task<bool> ValidateAsync(string key, string secret)
    {
        using (var context = new ApmsDbContext())
        {
            return await context.ApiKeys.AnyAsync(a => a.KeyId == key && a.Secret == secret);
        }
    }
}