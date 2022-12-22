using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.FileModels;
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
            if (!string.IsNullOrEmpty(service.Message))
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpGet("Categories/{id}")]
        public IActionResult GetProductCategoryById(int id)
        {
            var service = _productService.GetProductCategoryById(id);
            if (!string.IsNullOrEmpty(service.Message))
            {
                return NotFound(new { message = service.Message });
            }
            return Ok(service.Result);
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
            if (!string.IsNullOrEmpty(service.Message))
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpGet("SubCategories/{id}")]
        public IActionResult GetProductSubCategoryById(int id)
        {
            var service = _productService.GetProductSubCategoryById(id);
            if (!string.IsNullOrEmpty(service.Message))
            {
                return NotFound(new { message = service.Message});
            }
            return Ok(service.Result);
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



        [HttpPost("filter")]
        public IActionResult GetAllProducts(FilterModel model)
        {
            var service = _productService.GetProducts(model);
            if (service == null)
            {
                return BadRequest(new { message = "No Product Found!!!" });
            }
            return Ok(service.Result);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var service = _productService.GetProductById(id);
            if (!string.IsNullOrEmpty(service.Message))
            {
                return NotFound(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpPost]
        public async Task<ActionResult> PostProductAsync(ProductBindingModel model)
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

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadProductImgAsync([FromForm] FileModel model)
        {
            var result = await _productService.UploadProductImgAsync(model);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result.Result);
        }

        [HttpDelete("delete-image/{id}")]
        public async Task<IActionResult> DeleteProductImgAsync(int id)
        {
            var result = await _productService.DeleteProductImgAsync(id);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result.Success);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutProductAsync(int id, ProductBindingModel model)
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
