using ComandaPro.CrossCutting.Notifications;
using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Domain.Dtos.Product;
using Product.Domain.Filters;
using Product.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Product.API.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : Controller
    {
        private readonly IProductService _productService = productService;

        [HttpPost]
        [Route("create-product")]
        [SwaggerOperation(Summary = "Create product")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProductDto addProductDto)
        {
            var product = await _productService.AddProductAsync(addProductDto, this.GetUserIdLogged(), this.GetAccessToken());

            return Ok(product);
        }

        [HttpPut]
        [Route("update-product")]
        [SwaggerOperation(Summary = "Update product")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductDto updateProductDto)
        {
            var product = await _productService.UpdateProductAsync(updateProductDto);

            return Ok(product);
        }

        [HttpGet]
        [Route("get-product")]
        [SwaggerOperation(Summary = "Get product by id")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ProductDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProductAsync([FromQuery] int idProduct)
        {
            var product = await _productService.GetProductAsync(idProduct);

            if (product is null) return NotFound(new DefaultServiceResponseDto() { Message = StaticNotifications.ProductNotExists.Message, Success = true });
            return Ok(product);
        }

        [HttpPost]
        [Route("get-products")]
        [SwaggerOperation(Summary = "Get all products")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ICollection<ProductDto>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<Notification>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllProductsAsync([FromBody] ProductFilter filter)
        {
            var products = await _productService.GetAllProductsAsync(filter);

            if (products == null || !products.Any())
                return NotFound();

            return Ok(products);
        }

        [HttpDelete]
        [Route("delete-product")]
        [SwaggerOperation(Summary = "Delete product")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteProductAsync([FromQuery] int productId)
        {
            var response = await _productService.DeleteProductAsync(productId);
            return Ok(response);
        }
    }
}
