using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COMPTOIR.Controllers
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

        [HttpGet("Categories")]
        public IActionResult GetAllProductCategories()
        {
            var service = _productService.GetAllProductCategories();
            if (service == null)
            {
                return BadRequest(new { message = "No Category Found!!!" });
            }
            return Ok(service);
        }

        [HttpGet("Categories/{id}")]
        public IActionResult GetProductCategoryById(int id)
        {
            var service = _productService.GetProductCategoryById(id);
            if (service.Result == null)
            {
                return NotFound(new { message = "No Category Found!!!" });
            }
            return Ok(service);
        }

        [HttpPost("Categories")]
        public async Task<ActionResult> GetProductCategoryById(ProductCategory model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = await _productService.PostProductCategoryAsync(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }
        [HttpPut("Categories/{id}")]
        public async Task<ActionResult> PutProductCategoryAsync(int id, ProductCategory model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = await _productService.PutProductCategoryAsync(id, model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }


        [HttpGet("Categories/check-name")]
        public async Task<IActionResult> CheckCategoryName(string value)
        {
            var response = await _productService.CheckCategoryName(value);
            return Ok(response.Success);
        }
        [HttpGet("Categories/check-code")]
        public async Task<IActionResult> CheckCategoryCode(string value)
        {
            var response = await _productService.CheckCategoryCode(value);
            return Ok(response.Success);
        }

        [HttpGet("SubCategories")]
        public IActionResult GetAllProductSubCategories()
        {
            var service = _productService.GetAllProductSubCategories();
            if (service == null)
            {
                return BadRequest(new { message = "No SubCategory Found!!!" });
            }
            return Ok(service);
        }

        [HttpGet("SubCategories/{id}")]
        public IActionResult GetProductSubCategoryById(int id)
        {
            var service = _productService.GetProductSubCategoryById(id);
            if (service.Result == null)
            {
                return NotFound(new { message = "No SubCategory Found!!!" });
            }
            return Ok(service);
        }

        [HttpPost("SubCategories")]
        public async Task<ActionResult> GetProductSubCategoryById(ProductSubCategory model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = await _productService.PostProductSubCategoryAsync(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }
        [HttpPut("SubCategories/{id}")]
        public async Task<ActionResult> PutProductSubCategoryAsync(int id, ProductSubCategory model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = await _productService.PutProductSubCategoryAsync(id, model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpGet("SubCategories/check-name")]
        public async Task<IActionResult> CheckSubCategoryName(string value)
        {
            var response = await _productService.CheckSubCategoryName(value);
            return Ok(response.Success);
        }
        [HttpGet("SubCategories/check-code")]
        public async Task<IActionResult> CheckSubCategoryCode(string value)
        {
            var response = await _productService.CheckSubCategoryCode(value);
            return Ok(response.Success);
        }



        [HttpGet]
        public IActionResult GetAllProducts(FilterModel model)
        {
            var service = _productService.GetProducts(model);
            if (service == null)
            {
                return BadRequest(new { message = "No Product Found!!!" });
            }
            return Ok(service);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var service = _productService.GetProductById(id);
            if (service.Result == null)
            {
                return NotFound(new { message = "No Product Found!!!" });
            }
            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult> PostProductAsync(Product model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = await _productService.PostProductAsync(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProductAsync(int id, Product model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = await _productService.PutProductAsync(id, model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpGet("check-name")]
        public async Task<IActionResult> CheckProductName(string value)
        {
            var response = await _productService.CheckProductName(value);
            return Ok(response.Success);
        }
        [HttpGet("check-code")]
        public async Task<IActionResult> CheckProductCode(string value)
        {
            var response = await _productService.CheckProductCode(value);
            return Ok(response.Success);
        }
    }
}
