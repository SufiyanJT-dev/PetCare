using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.Command.MedicalEvent.AddMedicalEventCommand;
using PetCareManagement.Application.Command.MedicalEvent.DeleteMedicalEventCommand;
using PetCareManagement.Application.Command.MedicalEvent.UpdateMedicalEventCommand;

using PetCareManagement.Application.Query.Pets.GetAllEventByPetIdQuery;
using PetCareManagement.Application.Query.Pets.GetAllEventQuery;

namespace PetCareManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalEventController : ControllerBase
    {
        private readonly IMediator mediator;

        public MedicalEventController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> getAllEvent()
        {
            try
            {
                GetAllEventQuery query = new GetAllEventQuery();
                 
                   IEnumerable<Domain.Entity.MedicalEvent> enumerable= await mediator.Send(query);
                return Ok(enumerable);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
       [HttpPost("AddMedicalEvent")]
       
        public async Task<IActionResult> AddMedicalEvent(AddMedicalEventCommand command)
        {
            try
            {
                int result = await mediator.Send(command);
                return Ok(new { Message = "Medical event added successfully!" + result });
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
        [HttpGet("getByPetId/{petId}")]
        [Authorize]
        public async Task<IActionResult> GetByPetId(int petId)
        {
            try
            {
                GetAllEventByPetIdQuery query = new GetAllEventByPetIdQuery();
                query.PetId = petId;
                IEnumerable<Domain.Entity.MedicalEvent> result = await mediator.Send(query);
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
                DeleteMedicalEventCommand Command = new DeleteMedicalEventCommand();
                Command.EventId = id;
                bool result = await mediator.Send(Command);
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
        [HttpPatch("update/{Id}")]
        public async Task<IActionResult> Update( UpdateMedicalEventCommand command,int Id)
        {
            try
            {
                command.Id = Id;
                int result = await mediator.Send(command);
                return Ok(new { Message = "Medical event updated successfully!" + result });
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
