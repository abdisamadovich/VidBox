using VidBox.Service.Dtos.AdminAuth;

namespace VidBox.Service.Interfaces.AdminAuth;

public interface IAuthAdminService
{
    public Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterAdminDto dto);
    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone);
    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code);
    public Task<(bool Result, string Token)> LoginAsync(AdminDto dto);
}
