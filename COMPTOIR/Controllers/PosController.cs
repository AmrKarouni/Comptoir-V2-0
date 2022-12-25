using COMPTOIR.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COMPTOIR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosController : ControllerBase
    {
        private readonly IPosService _posService;
        public PosController(IPosService posService)
        {
            _posService = posService;
        }
        [HttpGet("Recipes")]
        public IActionResult GetAllPosRecipes()
        {
            var service = _posService.GetAllPosRecipes();
            if (!string.IsNullOrEmpty(service.Message))
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }
    }
}
