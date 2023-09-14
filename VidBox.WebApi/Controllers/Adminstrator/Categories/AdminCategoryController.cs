using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VidBox.Service.Dtos.Categories;
using VidBox.Service.Interfaces.Categories;
using VidBox.Service.Validators.Dtos.Categories;

namespace VidBox.WebApi.Controllers.Adminstrator.Categories;

[Route("api/admin/categories")]
[ApiController]
public class AdminCategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public AdminCategoryController(ICategoryService categoryService)
    {
        this._categoryService = categoryService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAsync([FromForm] CategoryCreateDto dto)
    {
        var createValidator = new CategoryCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _categoryService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }
}
