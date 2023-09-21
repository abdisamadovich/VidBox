using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VidBox.DataAccess.Utils;
using VidBox.Service.Interfaces.Categories;

namespace VidBox.WebApi.Controllers.Users.Categories;

[Route("api/user/category")]
[ApiController]
public class UserCategoryController : ControllerBase
{
    private readonly int maxPageSize = 10;
    private readonly ICategoryService _categoryService;

    public UserCategoryController(ICategoryService categoryService)
    {
        this._categoryService = categoryService;
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _categoryService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _categoryService.GetByIdAsync(id));

    [HttpGet("{id}/allVideo")]
    [AllowAnonymous]
    public async Task<IActionResult> GetVideosByCategory(long id, int page = 1)
    {
        var res = await _categoryService.GetVideosByCategory(id, new PaginationParams(page, maxPageSize));

        return Ok(res);
    }


}
