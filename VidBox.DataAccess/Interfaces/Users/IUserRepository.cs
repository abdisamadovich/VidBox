using VidBox.DataAccess.Common.Interfaces;
using VidBox.DataAccess.Utils;
using VidBox.DataAccess.ViewModels.Users;
using VidBox.Domain.Entities.Users;

namespace VidBox.DataAccess.Interfaces.Users
{
    public interface IUserRepository : IRepository<User,UserViewModel>,IGetAll<UserViewModel>
    {
        public Task<User?> GetByPhoneAsync(string phone);
        public Task<IList<UserViewModel>> SearchAsync(string search, PaginationParams @params);
    }
}
