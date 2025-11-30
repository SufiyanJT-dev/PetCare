using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.Command.Pets.Command.AddPetComand;
using PetCareManagement.Application.Command.Pets.Command.DeletePetCommand;
using PetCareManagement.Application.Command.Pets.Command.UpdatePetDeatilsCommand;
using PetCareManagement.Application.Query.Pets.GetPetOfUserId;


namespace PetCareManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PetsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("AddPet")]
        public async Task<IActionResult> AddPet(AddPetComand command)
        {
            try
            {
                int petId = await mediator.Send(command);
                return Ok(new { PetId = petId });
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpDelete("DeletePet{id}")]
        public async Task<ActionResult<bool>> DeletePet(int id)
        {
            try
            {
                DeletePetCommand command = new DeletePetCommand();
                command.PetId = id;
                return await mediator.Send(command);
            }
          
            catch( FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet("GetPetsByOwnerId{ownerId}")]
        public async Task<IActionResult> GetPetsByOwnerId(int ownerId)
        {
            try
            {
                var query =new GetPetOfUserIdQuery();
                query.Id = ownerId;
                var pets = await mediator.Send(query);
                return Ok(pets);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPatch("UpdatePetDetails{id}")]
        public async Task<ActionResult<int>> UpdatePetDeatails(int id,UpdatePetCommand command)
        {
            try
            {
                command.PetId = id;
                await mediator.Send(command);
                return Ok(new { Message = "Pet details updated successfully." });
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        }
}
