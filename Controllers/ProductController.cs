using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderManagementSystem.DTOs;
using SimpleOrderManagementSystem.Services;

namespace SimpleOrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [Route("admin")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductInputDTO input)
        {
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                try
                {

                    int ID = _productService.AddProduct(input);
                    return Ok(new { Message = "Product added successfully", ID });

                }
                catch (Exception ex)
                {

                    return StatusCode(500, new { Message = "An error occurred while adding the product", Error = ex.InnerException.Message });

                }
            }
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            // Validate page parameters
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest(new { Message = "Page number and page size must be greater than 0." });
            }
            var products = _productService.GetProducts(minPrice, maxPrice, pageNumber, pageSize);
            return Ok(products);
        }
    }
}
