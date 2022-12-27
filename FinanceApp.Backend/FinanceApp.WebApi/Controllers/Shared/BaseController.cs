using System.Security.Claims;
using FinanceApp.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.WebApi.Controllers.Shared;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected Guid UserId
    {
        get
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity) throw new UserNotFoundException();
            
            var userClaims = identity.Claims.ToArray();
            if (!userClaims.Any())
            {
                return Guid.Empty;
            }

            return Guid.Parse(userClaims.FirstOrDefault(x => x.Type == "userId")!.Value);
        }
    }

    protected bool DemoMode => UserId == new Guid("D3AC2B50-6CD3-4D38-8EC8-C8D3827FB3EF");
}