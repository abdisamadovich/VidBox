using VidBox.DataAccess.Utils;
using VidBox.DataAccess.ViewModels.Adminstrator;
using VidBox.Service.Dtos.Adminstrators;

namespace VidBox.Service.Interfaces.Adminstrators;

public interface IAdminstratorService
{
    public Task<bool> UpdateAsync(long adminId, AdminstratorUpdateDto dto);
    public Task<bool> DeleteAsync(long adminId);
}
