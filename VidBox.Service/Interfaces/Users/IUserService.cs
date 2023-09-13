using VidBox.DataAccess.Utils;
using VidBox.DataAccess.ViewModels.Users;
using VidBox.Service.Dtos.Users;

namespace VidBox.Service.Interfaces.Users;

public interface IUserService
{
    public Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params);
    public Task<bool> UpdateAsync(long UserId, UserUpdateDto dto);
    public Task<bool> DeleteAsync(long UserId);
    public Task<long> CountAsync();
    public Task<IList<UserViewModel>> SearchAsync(string search);
}
