using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.Auth.Dtos;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Auth.Query
{
    public class ValidateUserQueryHandler : IRequestHandler<ValidateUserQuery, ActionResult<AuthResult>>
    {
        private readonly IAuth _auth;
        private readonly IPasswordHasher<Domain.Entity.User> _passwordHasher;
        private readonly ITokenService _tokenService;

        public ValidateUserQueryHandler(IAuth auth, IPasswordHasher<Domain.Entity.User> passwordHasher, ITokenService tokenService)
        {
            _auth = auth;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<ActionResult<AuthResult>> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return new BadRequestObjectResult("Email and password are required.");

            var user = await _auth.FindByEmailAsync(request.Email, cancellationToken);
            if (user == null) return new NotFoundObjectResult("User not found.");

            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verifyResult == PasswordVerificationResult.Failed) return new UnauthorizedResult();

            if (verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                var newHash = _passwordHasher.HashPassword(user, request.Password);
                user.SetPasswordHash(newHash);
                await _auth.UpdateUserAsync(user, cancellationToken);
            }

            var accessToken = _tokenService.CreateAccessToken(user, out var accessExpiresAt);
            var refreshToken = _tokenService.CreateRefreshToken(out var refreshExpiresAt);

            var refreshTokenEntity = new RefreshToken(user.UserId, refreshToken, refreshExpiresAt);
            await _auth.SaveRefreshTokenAsync(refreshTokenEntity, cancellationToken);

            var result = new AuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = accessExpiresAt,
                RefreshTokenExpiresAt = refreshExpiresAt
            };

            return new OkObjectResult(result);
        }
    }

}
