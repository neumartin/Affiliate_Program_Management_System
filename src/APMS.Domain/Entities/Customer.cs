namespace APMS.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public int IdAffiliate { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string ZipCode { get; set; }
    public string ContactName { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// Associated affiliate
    /// </summary>
    public virtual Affiliate Affiliate { get; set; }
}