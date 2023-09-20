using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;
using VidBox.DataAccess.Utils;
using VidBox.Service.Dtos.Users;
using VidBox.Service.Interfaces.Categories;
using VidBox.Service.Interfaces.Users;
using VidBox.Service.Services.Categories;
using VidBox.Service.Validators.Dtos.Users;

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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _userService.GetAllAsync(new PaginationParams(page, maxPageSize)));

       /* public async Task<IActionResult> UpdateAsync(long userId, [FromForm] UserUpdateDto dto)
        {
            var updateValidator = new UserUpdateValidator();
            var validationResult = updateValidator.Validate(dto);
            if (validationResult.IsValid) return Ok(await _userService.UpdateAsync(userId, dto));
            else return BadRequest(validationResult.Errors);
        }*/

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CountAsync()
             => Ok(await _userService.CountAsync());

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long userId)
        => Ok(await _userService.DeleteAsync(userId));

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchAsync([FromQuery] string search)
        => Ok(await _userService.SearchAsync(search));
    }
}
