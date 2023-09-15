using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VidBox.Service.Interfaces.Categories;
using VidBox.Service.Interfaces.Users;
using VidBox.Service.Services.Categories;

namespace VidBox.WebApi.Controllers.Adminstrator.Users
{
    [Route("api/admin/user")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly int maxPageSize = 30;
        private readonly IUserService _userService;

        public AdminUserController(IUserService userService)
        {
            this._userService = userService;
        }
    }
}
