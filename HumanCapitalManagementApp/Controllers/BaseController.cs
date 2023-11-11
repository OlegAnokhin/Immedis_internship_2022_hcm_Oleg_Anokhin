namespace HumanCapitalManagementApp.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public abstract class BaseController : Controller
    {
        //New try to make Authorization
        protected string GetAuthenticationClaim()
        {
            return HttpContext.User.FindFirstValue(ClaimTypes.Authentication) == null
                ? HttpContext.Request.Headers["Authorization"]
                : HttpContext.User.FindFirstValue(ClaimTypes.Authentication);
        }
    }
}