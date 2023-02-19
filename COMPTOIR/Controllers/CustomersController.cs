using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.View;
using COMPTOIR.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COMPTOIR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpPost("Filter")]
        public IActionResult GetCustomersByFilter(FilterModel model)
        {
            var service = _customerService.GetCustomers(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }


        [HttpPost]
        public async Task<IActionResult> PostCustomerAsync(Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = await _customerService.PostCustomerAsync(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }
    }
}
