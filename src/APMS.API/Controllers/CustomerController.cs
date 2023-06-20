using APMS.API.ActionFilters;
using APMS.Domain.Entities;
using APMS.DTO;
using APMS.Managers.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Affiliate = APMS.Domain.Entities.Affiliate;

namespace APMS.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private IAffiliateManager _affiliateManager;
    private ICustomerManager _customerManager;

    public CustomerController(IAffiliateManager affiliateManager, ICustomerManager customerManager)
    {
        _affiliateManager = affiliateManager;
        _customerManager = customerManager;
    }

    [HttpGet("getAllByAffiliate/{idAffiliate:int}")]
    [ApiKeyActionFilter()]
    public async Task<List<CustomerDTO>> GetAllByAffiliate(int idAffiliate)
    {
        Affiliate affiliate = await _affiliateManager.FindByAsync(a => a.Id == idAffiliate);

        if (affiliate != null)
        {
            var custoemrs = await _customerManager.GetAllByAffiliate(idAffiliate);
            return custoemrs.Adapt<List<CustomerDTO>>();
        }
        else
            return null;
    }

    [HttpGet("{id:int}")]
    [ApiKeyActionFilter()]
    public async Task<IActionResult> Get(int id)
    {
        Customer customer = await _customerManager.FindByAsync(c => c.Id == id);

        if (customer != null)
        {
            return Ok(customer.Adapt<CustomerDTO>());
        }
        else
            return new StatusCodeResult(StatusCodes.Status404NotFound);
    }

    [HttpPost]
    [ApiKeyActionFilter()]
    public async Task<IActionResult> Post([FromBody] CustomerDTO customerDTO)
    {
        Affiliate affiliate = await _affiliateManager.FindByAsync(a => a.Id == customerDTO.IdAffiliate);

        if (affiliate == null)
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        
        Customer customer = customerDTO.Adapt<Customer>();
        await _customerManager.InsertAsync(customer);
        return Ok(customer.Adapt<CustomerDTO>());
    }

    [HttpPut]
    [ApiKeyActionFilter()]
    public async Task<IActionResult> Put([FromBody] CustomerDTO customerDTO)
    {
        Affiliate affiliate = await _affiliateManager.FindByAsync(a => a.Id == customerDTO.IdAffiliate);

        if (affiliate == null)
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        
        Customer customer = customerDTO.Adapt<Customer>();
        Customer oldCustomer = await _customerManager.FindByAsync(a => a.Id == customerDTO.Id);

        if (oldCustomer != null)
        {
            await _customerManager.UpdateAsync(customer);
            return Ok(customer.Adapt<CustomerDTO>());
        }
        else
            return new StatusCodeResult(StatusCodes.Status404NotFound);
    }
    
    [HttpDelete("{id:int}")]
    [ApiKeyActionFilter()]
    public async Task<IActionResult> Delete(int id)
    {
        Customer oldCustomer = await _customerManager.FindByAsync(a => a.Id == id);

        if (oldCustomer != null)
        {
            await _customerManager.DeleteAsync(oldCustomer);
            return new StatusCodeResult(StatusCodes.Status202Accepted);
        }
        else
            return new StatusCodeResult(StatusCodes.Status404NotFound);
    }
}