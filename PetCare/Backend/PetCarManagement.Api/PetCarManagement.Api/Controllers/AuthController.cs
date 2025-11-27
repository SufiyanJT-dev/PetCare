using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.Auth.Command;
using PetCareManagement.Application.Auth.Dtos;
using PetCareManagement.Application.Auth.Query;
using PetCareManagement.Application.IRepository;

namespace PetCareManagement.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

            [HttpPost("login")]
            public async Task<ActionResult<AuthResult>> Login([FromBody] ValidateUserQuery query)
            {
                try
                {
                    var result = await _mediator.Send(query);

                    Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = result.RefreshTokenExpiresAt
                    });

                    return Ok(result);
                }
                catch (FluentValidation.ValidationException ex)
                {
                    return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(new { Error = ex.Message });
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

            [HttpPost("refresh")]
            public async Task<ActionResult<AuthResult>> Refresh(CancellationToken ct)
            {
                var refreshToken = Request.Cookies["refreshToken"];
                if (string.IsNullOrEmpty(refreshToken))
                    return Unauthorized(new { Error = "Refresh token missing" });

                var command = new RefreshTokenCommand { RefreshToken = refreshToken };

                try
                {
                    var result = await _mediator.Send(command, ct);

                    Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = result.RefreshTokenExpiresAt
                    });

                    return Ok(result);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(new { Error = ex.Message });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = ex.Message });
                }
            }

        }

    }

