using Microsoft.AspNetCore.Mvc;
using VidBox.DataAccess.Utils;
using VidBox.Service.Interfaces.Categories;

namespace VidBox.WebApi.Controllers.Users.Categories;

[Route("api/category")]
[ApiController]
public class UserCategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public UserCategoryController(ICategoryService categoryService)
    {
        this._categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1, int PerPage = 10)
    => Ok(await _categoryService.GetAllAsync(new PaginationParams(page, PerPage)));

    [HttpGet("{id}")]   
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await _categoryService.GetByIdAsync(id));

}
