namespace APMS.Domain.Entities;

public class Affiliate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string ZipCode { get; set; }
    public string ContactName { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// All affiliate customers
    /// </summary>
    public virtual ICollection<Customer> Customers { get; set; }
}