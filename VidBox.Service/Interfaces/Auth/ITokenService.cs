using System.Diagnostics.Metrics;
using VidBox.Domain.Entities.Adminstrators;
using VidBox.Domain.Entities.Users;

namespace VidBox.Service.Interfaces.Auth;

public interface ITokenService
{
    public string GenerateToken(User user);
    public string GenerateToken(Adminstrator admin);
}
