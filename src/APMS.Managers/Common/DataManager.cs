using APMS.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace APMS.Managers.Common;

public class DataManager
{
    public void MigrateAndSeed()
    {

    }

    public void Initialize()
    {
        using (ApmsDbContext context = new ApmsDbContext())
        {
            context.Database.Migrate();
            context.Seed();
        }
    }
}