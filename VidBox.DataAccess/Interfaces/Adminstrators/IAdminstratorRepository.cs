using VidBox.DataAccess.Common.Interfaces;
using VidBox.DataAccess.ViewModels.Adminstrator;
using VidBox.Domain.Entities.Adminstrators;

namespace VidBox.DataAccess.Interfaces.Adminstrators
{
    public interface IAdminstratorRepository : IRepository<Adminstrator>,IGetByPhoneNumber<Adminstrator?>
    {
        public Task<AdminstratorViewModel?> GetByIdViewModelAsync(long id);
    }
}
