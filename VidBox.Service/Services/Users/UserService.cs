using VidBox.DataAccess.Interfaces.Users;
using VidBox.DataAccess.Utils;
using VidBox.DataAccess.ViewModels.Users;
using VidBox.Domain.Entities.Users;
using VidBox.Domain.Exceptions.Users;
using VidBox.Service.Common.Helpers;
using VidBox.Service.Dtos.Users;
using VidBox.Service.Interfaces.Common;
using VidBox.Service.Interfaces.Users;

namespace VidBox.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPaginator _paginator;
    private readonly IFileService _fileService;

    public UserService(IUserRepository userRepository,
        IPaginator paginator,
        IFileService fileService)
    {
        this._userRepository = userRepository;
        this._paginator = paginator;
        this._fileService = fileService;
    }
    public async Task<long> CountAsync()
    {
        var result = await _userRepository.CountAsync();
        return result;
    }

    public async Task<bool> DeleteAsync(long UserId)
    {
        var user = await _userRepository.GetByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException();

        var dbResult = await _userRepository.DeleteAsync(UserId);

        return dbResult > 0;
    }

    public async Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        var users = await _userRepository.GetAllAsync(@params);
        var count = await _userRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return users;
    }

    public async Task<IList<UserViewModel>> SearchAsync(string search)
    {
        var searches = await _userRepository.SearchAsync(search);
        return searches;
    }

    public async Task<bool> UpdateAsync(long UserId, UserUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(UserId);
        if (user == null) throw new UserNotFoundException();
        user.Name = dto.Name;
        user.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _userRepository.UpdateAsync(UserId, user);

        return dbResult > 0;
    }
}
