using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VidBox.DataAccess.Utils;
using VidBox.Service.Dtos.Categories;
using VidBox.Service.Interfaces.Categories;
using VidBox.Service.Validators.Dtos.Categories;

namespace VidBox.WebApi.Controllers.Adminstrator.Categories
{
    [Route("api/admin/category")]
    [ApiController]
    public class AdminCategoryController : ControllerBase
    {
        private readonly int maxPageSize = 10;
        private readonly ICategoryService _categoryService;

        public AdminCategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromForm] CategoryCreateDto dto)
        {
            var createValidator = new CategoryCreateValidator();
            var result = createValidator.Validate(dto);
            if (result.IsValid) return Ok(await _categoryService.CreateAsync(dto));
            else return BadRequest(result.Errors);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _categoryService.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long categoryId)
            => Ok(await _categoryService.GetByIdAsync(categoryId));

        [HttpPut("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long categoryId, [FromForm] CategoryUpdateDto dto)
        {
            var updateValidator = new CategoryUpdateValidator();    
            var validationResult = updateValidator.Validate(dto);
            if (validationResult.IsValid) return Ok(await _categoryService.UpdateAsync(categoryId, dto));
            else return BadRequest(validationResult.Errors);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long categoryId)
        => Ok(await _categoryService.DeleteAsync(categoryId));

        [HttpGet("allVideo/byCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVideosByCategory(long categoryId,int page = 1)
        {
            var res = await _categoryService.GetVideosByCategory(categoryId, new PaginationParams(page, maxPageSize));
            
            return Ok(res);
        }
    }
}
