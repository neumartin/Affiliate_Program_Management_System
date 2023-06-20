using APMS.Managers.Interfaces;
using APMS.Managers.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APMS.API.ActionFilters;

public class ApiKeyActionFilter : ActionFilterAttribute
{
    private IApiKeyManager _apiKeyManager = new ApiKeyManager();

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string apiKey = context.HttpContext.Request.Headers["api-key"];
        string secret = context.HttpContext.Request.Headers["secret"];
        bool valid = await _apiKeyManager.ValidateAsync(apiKey, secret);

        if (!valid)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            return;
        }

        await next();
    }
}