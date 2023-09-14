using VidBox.DataAccess.Interfaces.Adminstrators;
using VidBox.Domain.Exceptions.Adminstrators;
using VidBox.Service.Dtos.Adminstrators;
using VidBox.Service.Interfaces.Adminstrators;
using VidBox.Service.Interfaces.Common;
using VidBox.Service.Interfaces.Identity;

namespace VidBox.Service.Services.Adminstrator;

public class AdminstratorService : IAdminstratorService
{
    private readonly IAdminstratorRepository _administratorRepository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;
    private readonly IIdentityService _identity;

    public AdminstratorService(IAdminstratorRepository administrator,
        IFileService fileService,
        IPaginator paginator,
        IIdentityService identityService)
    {
        this._administratorRepository = administrator;
        this._fileService = fileService;
        this._paginator = paginator;
        this._identity = identityService;
    }

    public async Task<bool> DeleteAsync(long adminId)
    {
        var administrator = await _administratorRepository.GetByIdAsync(adminId);
        if (administrator is null) throw new AdminstratorNotFoundException();
        var dbResult = await _administratorRepository.DeleteAsync(adminId);

        return dbResult > 0;
    }

    public async Task<bool> UpdateAsync(long adminId, AdminstratorUpdateDto dto)
    {
        var administrator = await _administratorRepository.GetByIdAsync(adminId);
        if (administrator == null) throw new AdminstratorNotFoundException();
        administrator.Name = dto.Name;
        var dbResult = await _administratorRepository.UpdateAsync(adminId, administrator);

        return dbResult > 0;
    }
}
