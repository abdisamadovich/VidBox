using Microsoft.AspNetCore.Http;
using VidBox.Service.Interfaces.Identity;

namespace VidBox.Service.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _accessor;
    public IdentityService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public long UserId
    {
        get
        {
            if (_accessor.HttpContext is null) return 0;
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "Id");
            if (claim == null) return 0;
            else return long.Parse(claim.Value);
        }
    }

    public string Name
    {
        get
        {
            if (_accessor.HttpContext is null) return "";
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "Name");
            if (claim is null) return "";
            else return claim.Value;
        }
    }

    public string PhoneNumber
    {
        get
        {
            if (_accessor.HttpContext is null) return "";
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "PhoneNumber");
            if (claim is null) return "";
            else return claim.Value;
        }
    }

    public string IdentityRole
    {
        get
        {
            if (_accessor.HttpContext is null) return null;
            string type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == type);
            if (claim is null) return null;
            else return claim.Value;
        }
    }
}
