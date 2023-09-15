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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await _categoryService.GetByIdAsync(id));

}
