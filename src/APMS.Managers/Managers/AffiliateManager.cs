using APMS.DataAccess.Context;
using APMS.Domain.Entities;
using APMS.Managers.Common;
using APMS.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APMS.Managers.Managers;

public class AffiliateManager : GenericManager<Affiliate>, IAffiliateManager
{
    public async Task<int> CustoemrsCount(int idAffiliate)
    {
        using (var context = new ApmsDbContext())
        {
            return await context
                .Customers
                .CountAsync(c => c.IdAffiliate == idAffiliate);
        }
    }
}