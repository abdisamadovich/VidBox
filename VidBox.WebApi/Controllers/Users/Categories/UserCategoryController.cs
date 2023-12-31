﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VidBox.DataAccess.Utils;
using VidBox.Service.Interfaces.Categories;

namespace VidBox.WebApi.Controllers.Users.Categories;

[Route("api/user/categories")]
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
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _categoryService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{categoryId}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetByIdAsync(long categoryId)
            => Ok(await _categoryService.GetByIdAsync(categoryId));

    [HttpGet("{categoryId}/videos")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetVideosByCategory(long categoryId, int page = 1)
    {
        var res = await _categoryService.GetVideosByCategory(categoryId, new PaginationParams(page, maxPageSize));

        return Ok(res);
    }


}
