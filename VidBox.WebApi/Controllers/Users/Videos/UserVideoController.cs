using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VidBox.DataAccess.Utils;
using VidBox.Service.Interfaces.Identity;
using VidBox.Service.Interfaces.Videos;

namespace VidBox.WebApi.Controllers.Users.Videos;

[Route("api/user/video")]
[ApiController]
public class UserVideoController : ControllerBase
{

    private readonly int maxPageSize = 10;
    private readonly IVideoService _videoService;
    private readonly IIdentityService _identity;

    public UserVideoController(IVideoService VideoService,
            IIdentityService identity)
    {
        _videoService = VideoService;
        _identity = identity;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _videoService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long id)
    => Ok(await _videoService.GetByIdAsync(id));

    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<IActionResult> SearchAsync([FromQuery] string search)
        => Ok(await _videoService.SearchAsync(search));
}
