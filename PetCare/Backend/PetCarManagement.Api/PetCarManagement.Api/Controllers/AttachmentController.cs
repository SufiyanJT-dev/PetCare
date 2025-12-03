using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.Command.Attachment.CreateAttachmentCommand;
using PetCareManagement.Application.Command.Attachment.DeleteAttachmentCommand;
using PetCareManagement.Application.Command.Attachment.UpdateAttachmentCommand;
using PetCareManagement.Application.Dos.Attachment;
using PetCareManagement.Application.Query.Attachment.GetAllAttachment;
using PetCareManagement.Application.Query.Attachment.GetAllAttachmentByPetId;
using PetCareManagement.Application.Query.Attachment.GetAllByEventIdAttachment;

namespace PetCareManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttachmentController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> GetAttachments([FromForm] CreateAttachmentCommand command)
        {
            try
            {
               
                int result = await  _mediator.Send(command);
                return Ok(new { AttachmentId = result });

            }
            catch(FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            try
            {
                DeleteAttachmentCommand command = new DeleteAttachmentCommand();
                command.AttachmentId = id;
                await _mediator.Send(command);
                return Ok(new { Message = "Attachment deleted successfully." });
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAttachment(int id, [FromForm] AttachmentUpdateDtos command)
        {
            try
            {
                UpdateAttachmentCommand updateCommand = new UpdateAttachmentCommand
                {
                    Id = id,
                    MedicalEventId = command.MedicalEventId,
                    Description = command.Description,
                    File=command.File,
                    FileName = command.FileName
                };
            
            int result = await _mediator.Send(updateCommand);
                return Ok(new { AttachmentId = result });
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAttachments()
        {
            try
            {
                GetAllAttachmentQuery query = new GetAllAttachmentQuery();
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
        [HttpGet("GetByEventId{Id}")]
        public async Task<IActionResult> GetAllAttachmentsByEventId(int Id)
        {
            try
            {
                GetAllAttachmentByEventIdQuery query = new GetAllAttachmentByEventIdQuery();
                query.EventId = Id;
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
        [HttpGet("GetByPetId{Id}")]
        public async Task<IActionResult> GetAllAttachmentsByPetId(int Id)
        {
            try
            {
                GetAllAttachmentByPetIdQuery query = new GetAllAttachmentByPetIdQuery();
                query.PetId = Id;
                 
                return Ok(await _mediator.Send(query));
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
