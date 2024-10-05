using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Core.Services.Contract;

namespace Store.G04.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // when i call this endpoint => BaseUrl/api/Products
        // products is the name of the controller
        [HttpGet]
        // i don't use the name of end point 
        //                          ||
        //                           V
        public async Task<IActionResult> GetAllProducts() // endpoint
        {
            var result = await _productService.GetAllProductsAsync();

            return Ok(result); // return status code is 200  and result
        }

        [HttpGet("brands")]

        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();

            return Ok(result);
        }

        [HttpGet("types")]

        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id == null) return BadRequest("Invalid Id !!");

            var result = await _productService.GetProductByIdAsync(id.Value);

            if (result == null) return NotFound($"The Product with Id : {id} not found at DB");

            return Ok(result);
        }
    }
}
