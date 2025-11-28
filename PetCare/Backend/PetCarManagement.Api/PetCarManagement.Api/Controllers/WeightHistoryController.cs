using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.WeightHistory.Command.DeleteWeightHistory;
using PetCareManagement.Application.WeightHistory.Query.GetAllWeightHistoryByPetId;

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
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteWeightHistoryCommand { WhId = id });
                return Ok(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] PetCareManagement.Application.WeightHistory.Command.UpdateWeightHistory.UpdateWeightHistoryCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpGet("bypet/{petId}")]
        public async Task<IActionResult> GetAllByPetId(int petId)
        {
            try
            {
                var query = new GetAllWeightHistoryByPetIdQuery
                {
                    PetId = petId  // set the property manually
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
