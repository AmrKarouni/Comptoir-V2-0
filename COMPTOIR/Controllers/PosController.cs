using COMPTOIR.Models.Binding;
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
        [HttpPost("Tickets")]
        public async Task<IActionResult> PostPosTicket(TicketBindingModel model)
        {
            var service = await _posService.PostPosTicket(model);
            if (!string.IsNullOrEmpty(service.Message))
            {
                return BadRequest(new { message = service.Result });
            }
            return Ok(service.Result);
        }

        [HttpGet("Tickets/{id}")]
        public IActionResult GetTicketById(int id)
        {
            var service = _posService.GetTicketById(id);
            if (!string.IsNullOrEmpty(service.Message))
            {
                return BadRequest(new { message = service.Result });
            }
            return Ok(service.Result);
        }


        [HttpPut("Tickets/{id}")]
        public async Task<IActionResult> PutPosTicket(int id,TicketBindingModel model)
        {
            var service = await _posService.PutPosTicket(id,model);
            if (!string.IsNullOrEmpty(service.Message))
            {
                return BadRequest(new { message = service.Result});
            }
            return Ok(service.Result);
        }

        [HttpPost("Tickets/Pay")]
        public async Task<IActionResult> DeliverPosTicket(TicketDeliverBindingModel model)
        {
            var service = await _posService.DeliverPosTicket(model);
            if (!string.IsNullOrEmpty(service.Message))
            {
                return BadRequest(new { message = service.Result});
            }
            return Ok(service.Result);
        }

        [HttpGet("Tickets/Pending")]
        public IActionResult GetTodayPendingTickets()
        {
            var service = _posService.GetTodayPendingTickets();
            if (!string.IsNullOrEmpty(service.Message))
            {
                return BadRequest(new { message = service.Result });
            }
            return Ok(service.Result);
        }
        [HttpGet("Tickets/Pending/{channelId}")]
        public IActionResult GetTodayPendingTicketsByChannelId(int channelId)
        {
            var service = _posService.GetTodayPendingTicketsByChannelId(channelId);
            if (!string.IsNullOrEmpty(service.Message))
            {
                return BadRequest(new { message = service.Result });
            }
            return Ok(service.Result);
        }
    }
}
