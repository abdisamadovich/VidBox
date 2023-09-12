using VidBox.DataAccess.ViewModels.Adminstrator;
using VidBox.Domain.Entities.Adminstrators;

namespace VidBox.DataAccess.Interfaces.Adminstrators
{
    public interface IAdminstratorRepository : IRepository<Adminstrator, AdminstratorViewModel>
    {}
}
