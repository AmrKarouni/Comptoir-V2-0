using COMPTOIR.Models.AppModels;
using COMPTOIR.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COMPTOIR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        public PlacesController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet("Categories")]
        public IActionResult GetAllPlaceCategories()
        {
            var service = _placeService.GetAllPlaceCategories();
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpGet("Categories/{id}")]
        public IActionResult GetPlaceCategoryById(int id)
        {
            var service = _placeService.GetPlaceCategoryById(id);
            if (!service.Success)
            {
                return NotFound(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpPost("Categories")]
        public async Task<ActionResult> PostPlaceCategoryAsync(PlaceCategory model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = await _placeService.PostPlaceCategoryAsync(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }




        [HttpPost]
        public async Task<ActionResult> PostPlaceAsync(Place model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = await _placeService.PostPlaceAsync(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }


        [HttpGet]
        public IActionResult GetAllPlaces()
        {
            var service = _placeService.GetAllPlaces();
            if (!service.Success)
            {
                return NotFound(new { message = service.Message });
            }
            return Ok(service.Result);
        }


        [HttpGet("{id}")]
        public IActionResult GetPlaceById(int id)
        {
            var service = _placeService.GetPlaceById(id);
            if (!service.Success)
            {
                return NotFound(new { message = service.Message });
            }
            return Ok(service.Result);
        }


        [HttpGet("ByCategory/{id}")]
        public IActionResult GetPlaceByCategoryId(int id)
        {
            var service = _placeService.GetPlaceByCategoryId(id);
            if (!service.Success)
            {
                return NotFound(new { message = service.Message });
            }
            return Ok(service.Result);
        }
    }
}
