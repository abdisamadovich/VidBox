using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VidBox.DataAccess.Utils;
using VidBox.Service.Dtos.Videos;
using VidBox.Service.Interfaces.Identity;
using VidBox.Service.Interfaces.Videos;

namespace VidBox.WebApi.Controllers.Adminstrator.Videos;

[Route("api/admin")]
[ApiController]
public class AdminVideoController : ControllerBase
{
    private readonly int maxPageSize = 10;
    private readonly IVideoService _service;
    private readonly IIdentityService _identity;

    public AdminVideoController(IVideoService VideoService,
            IIdentityService identity)
    {
        _service = VideoService;
        _identity = identity;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] VideoCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
}
