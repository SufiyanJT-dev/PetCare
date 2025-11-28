using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetCareManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeightHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateWeightHistory([FromBody] PetCareManagement.Application.WeightHistory.Command.CreateWeightHistory.CreateWeightHistoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
