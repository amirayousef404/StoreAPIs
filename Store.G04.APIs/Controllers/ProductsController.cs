using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Errors;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Helper;
using Store.G04.Core.Services.Contract;
using Store.G04.Core.Specifications.Products;

namespace Store.G04.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // when i call this endpoint => BaseUrl/api/Products
        // products is the name of the controller

        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet]
        // i don't use the name of end point 
        //                          ||
        //                           V
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpec) // endpoint
        {
            var result = await _productService.GetAllProductsAsync(productSpec);

            return Ok(result); // return status code is 200  and result
        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")]

        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();

            return Ok(result);
        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")]

        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();

            return Ok(result);
        }

        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]

        public async Task<ActionResult<ProductDto>> GetProductById(int? id)
        {
            if (id == null) return BadRequest(new ApiErrorResponse(400));

            var result = await _productService.GetProductByIdAsync(id.Value);

            if (result == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, $"The Product with Id : {id} not found at DB"));

            return Ok(result);
        }
    }
}
