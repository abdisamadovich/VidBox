using VidBox.DataAccess.Common.Interfaces;
using VidBox.DataAccess.Utils;
using VidBox.DataAccess.ViewModels.Users;
using VidBox.Domain.Entities.Users;

namespace VidBox.DataAccess.Interfaces.Users
{
    public interface IUserRepository : IRepository<User>,IGetAll<UserViewModel>,IGetByPhoneNumber<User?>,
        ISearchable<UserViewModel>
    {
        public Task<UserViewModel?> GetByIdViewAsync(long id);  
    }
}
