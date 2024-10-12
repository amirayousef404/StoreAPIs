using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Errors;
using Store.G04.Repository.Data.Contexts;

namespace Store.G04.APIs.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]

        public IActionResult GetNotFoundErrorRequest()
        {
            var brand = _context.Brands.Find(100);

            if (brand is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "brand with id is not found"));

            return Ok(brand);
        }

        [HttpGet("servererror")]

        public IActionResult GetServerErrorRequest()
        {
            var brand = _context.Brands.Find(100);

            var brandToString = brand.ToString();

            return Ok(brand);
        }

        [HttpGet("badrequest")]

        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400));

            
        }

        [HttpGet("badrequest/{id}")]

        public IActionResult GetBadRequest(int id)
        {
            if(!ModelState.IsValid) return BadRequest();    
            
            return Ok();
        }

        [HttpGet("unauthorized")]

        public IActionResult GetUnauthorized()
        { 
            
            return Unauthorized(new ApiErrorResponse(401));
        }
    }
}
