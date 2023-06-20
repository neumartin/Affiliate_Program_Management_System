using APMS.DataAccess.Context;
using APMS.Domain.Entities;
using APMS.Managers.Common;
using APMS.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APMS.Managers.Managers;

public class CustomerManager : GenericManager<Customer>, ICustomerManager
{
    public async Task<List<Customer>> GetAllByAffiliate(int idAffiliate)
    {
        using (var context = new ApmsDbContext())
        {
            return await context.Customers
                .Where(c => c.IdAffiliate == idAffiliate)
                .ToListAsync();
        }
    }
}