using VidBox.Domain.Entities.Users;

namespace VidBox.Service.Interfaces.AdminAuth;

public interface ITokenAdminService
{
    public string GenerateToken(User user);
}
