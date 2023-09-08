using VidBox.DataAccess.Common.Interfaces;
using VidBox.DataAccess.ViewModels.Users;
using VidBox.Domain.Entities.Users;

namespace VidBox.DataAccess.Interfaces.Users
{
    public interface IUserRepository : IRepository<User,UserViewModel>,IGetAll<UserViewModel>, 
        ISearchable<UserViewModel>
    {}
}
