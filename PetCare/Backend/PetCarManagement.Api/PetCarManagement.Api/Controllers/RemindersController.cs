using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.Command.Reminder.CreateCommand;
using PetCareManagement.Application.Command.Reminder.DeleteCommand;
using PetCareManagement.Application.Command.Reminder.UpdateCommand;
using PetCareManagement.Application.Query.Reminder.GetAllpreviousReminderByPetId;
using PetCareManagement.Application.Query.Reminder.GetAllReminderByPetId;
using PetCareManagement.Domain.Entity;

namespace PetCareManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : ControllerBase
    {
        private readonly IMediator mediator;

        public RemindersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("CreateReminder")]
        public async Task<IActionResult> CreateReminder(CreateReminderCommand command)
        {
            try
            {
                int reminderId = await mediator.Send(command);
                return new OkObjectResult(new { ReminderId = reminderId });
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
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteReminder(int Id)
        {
            try
            {
                DeleteReminderCommand command = new DeleteReminderCommand();
                command.Id = Id;
                int reminderId = await mediator.Send(command);
                return Ok(new { Message = "Reminder deleted successfully. " + reminderId });
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
        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateReminder(int Id, UpdateReminderCommand command)
        {
            try
            {
                command.ReminderId = Id;
                int reminderId = await mediator.Send(command);
                return Ok(new { Message = "Reminder updated successfully. " + reminderId });
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
        [HttpGet("GetReminderByPetId/{Id}")]
        public async Task<IActionResult> GetReminderByPetId(int Id)
        {
            try
            {
                GetAllReminderByPetIdQuery query = new GetAllReminderByPetIdQuery();
                query.PetId = Id;
                IEnumerable<MedicalEvent> reminders = await mediator.Send(query);
                return Ok(reminders);
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
        [HttpGet("GetPreviousReminders/{Id}")]
        public async Task<IActionResult> GetPreviousReminders(int Id)
        {
            try
            {
                GetAllpreviousReminderByPetIdQuery query = new GetAllpreviousReminderByPetIdQuery();
                query.PetId = Id;
                IEnumerable<MedicalEvent> reminders = await mediator.Send(query);
                return Ok(reminders);
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
