using ComandaPro.CrossCutting.Notifications;
using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Domain.Dtos.Category;
using Product.Domain.Dtos.Product;
using Product.Domain.Filters;
using Product.Domain.Interfaces.Services;
using Product.Service.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Product.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController(ICategoryService categoryService) : Controller
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost]
        [Route("create-category")]
        [SwaggerOperation(Summary = "Create category")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddCategoryAsync([FromBody] AddCategoryDto addCategoryDto)
        {
            var category = await _categoryService.AddCategoryAsync(addCategoryDto, this.GetUserIdLogged(), this.GetAccessToken());

            return Ok(category);
        }

        [HttpPost]
        [Route("get-categories")]
        [SwaggerOperation(Summary = "Get all categories")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ICollection<CategoryDto>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<Notification>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllCategoriesAsync([FromBody] CategoryFilter filter)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(filter);

            if (categories == null || !categories.Any())
                return NotFound();

            return Ok(categories);
        }

        [HttpDelete]
        [Route("delete-category")]
        [SwaggerOperation(Summary = "Delete category")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteCategoryAsync([FromQuery] int categoryId)
        {
            var response = await _categoryService.DeleteCategoryAsync(categoryId);
            return Ok(response);
        }
    }
}
