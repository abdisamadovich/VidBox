using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VidBox.DataAccess.Utils;
using VidBox.Service.Dtos.Videos;
using VidBox.Service.Interfaces.Identity;
using VidBox.Service.Interfaces.Videos;

namespace VidBox.WebApi.Controllers.Adminstrator.Videos;

[Route("api/admin/video")]
[ApiController]
public class AdminVideoController : ControllerBase
{
    private readonly int maxPageSize = 10;
    private readonly IVideoService _videoService;
    private readonly IIdentityService _identity;

    public AdminVideoController(IVideoService VideoService,
            IIdentityService identity)
    {
        _videoService = VideoService;
        _identity = identity;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _videoService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromForm] VideoCreateDto dto)
    {
        return Ok(await _videoService.CreateAsync(dto));
    }

    [HttpGet("{vidoId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long vidoId)
        => Ok(await _videoService.GetByIdAsync(vidoId));

    [HttpDelete("{videoId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long videoId)
        => Ok(await _videoService.DeleteAsync(videoId));

    [HttpPut("{videoId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(long videoId, [FromForm] VideoUpdateDto dto)
    {
        return Ok(await _videoService.UpdateAsync(videoId, dto));
    }

    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<IActionResult> SearchAsync([FromQuery] string search)
            => Ok(await _videoService.SearchAsync(search));

    [HttpGet("count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountAsync()
        => Ok(await _videoService.CountAsync());

}
