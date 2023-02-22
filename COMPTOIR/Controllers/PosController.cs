using COMPTOIR.Models.Binding;
using COMPTOIR.Models.View;
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
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpGet("Taxes/{id}")]
        public IActionResult GetTaxesByChannelId(int id)
        {
            var service = _posService.GetTaxesByChannelId(id);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }


        [HttpPost("Customers")]
        public IActionResult GetCustomersByFilter(FilterModel model)
        {
            var service = _posService.GetCustomersByFilter(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpPost("Tickets")]
        public async Task<IActionResult> PostPosTicket(TicketBindingModel model)
        {
            var service = await _posService.PostPosTicket(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpGet("Tickets/{id}")]
        public IActionResult GetTicketById(int id)
        {
            var service = _posService.GetTicketById(id);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }


        [HttpPut("Tickets/{id}")]
        public async Task<IActionResult> PutPosTicket(int id,TicketBindingModel model)
        {
            var service = await _posService.PutPosTicket(id,model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpPost("Tickets/Pay")]
        public async Task<IActionResult> DeliverPosTicket(TicketDeliverBindingModel model)
        {
            var service = await _posService.DeliverPosTicket(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpDelete("Tickets/Cancel/{id}")]
        public IActionResult CancelTicket(int id)
        {
            var service =  _posService.CancelTicket(id);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Success);
        }

        [HttpGet("Tickets/Pending")]
        public IActionResult GetTodayPendingTickets()
        {
            var service = _posService.GetTodayPendingTickets();
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }
        [HttpGet("Tickets/Pending/{channelId}")]
        public IActionResult GetTodayPendingTicketsByChannelId(int channelId)
        {
            var service = _posService.GetTodayPendingTicketsByChannelId(channelId);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }
        [HttpPost("Tickets/Filter")]
        public IActionResult GetPosTicketsByFilter(FilterModel model)
        {
            var service = _posService.GetPosTicketsByFilter(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }

        [HttpPost("Tickets/Actions")]
        public async Task<IActionResult> PosTicketActions(TicketPayBindingModel model)
        {
            var service = await _posService.PosTicketActions(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }


        [HttpPost("Tickets/Refund")]
        public async Task<IActionResult> RefundTicketAsync(TicketPayBindingModel model)
        {
            var service = await _posService.RefundTicketAsync(model);
            if (!service.Success)
            {
                return BadRequest(new { message = service.Message });
            }
            return Ok(service.Result);
        }
    }
}
