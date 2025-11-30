using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.Dos.Auth;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Auth
{
    public class ValidateUserQueryHandler : IRequestHandler<ValidateUserQuery, AuthResult>
    {
        private readonly IAuth _auth;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;

        public ValidateUserQueryHandler(IAuth auth, IPasswordHasher<User> passwordHasher, ITokenService tokenService)
        {
            _auth = auth;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Email and password are required.");

            var user = await _auth.FindByEmailAsync(request.Email, cancellationToken);
            if (user == null) throw new KeyNotFoundException("User not found.");

            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verifyResult == PasswordVerificationResult.Failed) throw new UnauthorizedAccessException("Invalid credentials.");

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

         

            return  new AuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = accessExpiresAt,
                RefreshTokenExpiresAt = refreshExpiresAt
            };
        }
    }

}
