using APMS.Domain.Entities;
using APMS.Managers.Common;

namespace APMS.Managers.Interfaces;

public interface IAffiliateManager : IManager<Affiliate>
{
    Task<int> CustoemrsCount(int idAffiliate);
}