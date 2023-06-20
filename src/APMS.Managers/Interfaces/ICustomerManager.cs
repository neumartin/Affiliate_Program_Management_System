using APMS.Domain.Entities;
using APMS.Managers.Common;

namespace APMS.Managers.Interfaces;

public interface ICustomerManager : IManager<Customer>
{
    Task<List<Customer>> GetAllByAffiliate(int idAffiliate);
}