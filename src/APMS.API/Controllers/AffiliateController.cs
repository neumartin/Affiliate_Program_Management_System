using APMS.API.ActionFilters;
using APMS.DTO;
using APMS.Managers.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Affiliate = APMS.Domain.Entities.Affiliate;

namespace APMS.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AffiliateController : ControllerBase
{
    private IAffiliateManager _affiliateManager;

    public AffiliateController(IAffiliateManager affiliateManager)
    {
        _affiliateManager = affiliateManager;
    }

    [HttpGet("getAll")]
    [ApiKeyActionFilter()]
    public async Task<List<AffiliateDTO>> GetAll()
    {
        var affiliates = await _affiliateManager.GetAllAsync();
        return affiliates.Adapt<List<AffiliateDTO>>();
    }

    [HttpGet("{id:int}")]
    [ApiKeyActionFilter()]
    public async Task<IActionResult> Get(int id)
    {
        Affiliate affiliate = await _affiliateManager.FindByAsync(a => a.Id == id);

        if (affiliate != null)
        {
            return Ok(affiliate.Adapt<AffiliateDTO>());
        }
        else
            return new StatusCodeResult(StatusCodes.Status404NotFound);
    }

    [HttpGet("customersCount/{id:int}")]
    [ApiKeyActionFilter()]
    public async Task<IActionResult> CustomersCount(int id)
    {
        Affiliate affiliate = await _affiliateManager.FindByAsync(a => a.Id == id);

        if (affiliate != null)
        {
            int count = await _affiliateManager.CustoemrsCount(id);
            return Ok(new {customersCount = count});
        }
        else
            return new StatusCodeResult(StatusCodes.Status404NotFound);
    }
    
    [HttpPost]
    [ApiKeyActionFilter()]
    public async Task<IActionResult> Post([FromBody] AffiliateDTO affiliateDTO)
    {
        Affiliate affiliate = affiliateDTO.Adapt<Affiliate>();
        await _affiliateManager.InsertAsync(affiliate);
        return Ok(affiliate.Adapt<AffiliateDTO>());
    }

    [HttpPut]
    [ApiKeyActionFilter()]
    public async Task<IActionResult> Put([FromBody] AffiliateDTO affiliateDTO)
    {
        Affiliate affiliate = affiliateDTO.Adapt<Affiliate>();
        Affiliate oldAffiliate = await _affiliateManager.FindByAsync(a => a.Id == affiliateDTO.Id);

        if (oldAffiliate != null)
        {
            await _affiliateManager.UpdateAsync(affiliate);
            return Ok(affiliate.Adapt<AffiliateDTO>());
        }
        else
            return new StatusCodeResult(StatusCodes.Status404NotFound);
    }
    
    [HttpDelete("{id:int}")]
    [ApiKeyActionFilter()]
    public async Task<IActionResult> Delete(int id)
    {
        Affiliate oldAffiliate = await _affiliateManager.FindByAsync(a => a.Id == id);

        if (oldAffiliate != null)
        {
            await _affiliateManager.DeleteAsync(oldAffiliate);
            return new StatusCodeResult(StatusCodes.Status202Accepted);
        }
        else
            return new StatusCodeResult(StatusCodes.Status404NotFound);
    }
}