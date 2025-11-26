using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetCareManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator) 
        {
            this.mediator = mediator;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(Application.User.command.CreateUserCommand command)
        {
            try
            {
                var userId = await mediator.Send(command);

                return Ok(new
                {
                    UserId = userId,
                    Message = "User registered successfully!"
                });
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
