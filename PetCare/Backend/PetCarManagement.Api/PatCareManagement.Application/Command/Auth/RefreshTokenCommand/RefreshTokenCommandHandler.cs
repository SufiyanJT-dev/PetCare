using MediatR;
using PetCareManagement.Application.Dos.Auth;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;

namespace PetCareManagement.Application.Command.Auth.Command
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResult>
    {
        private readonly IAuth _authRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(IAuth authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> Handle(RefreshTokenCommand request, CancellationToken ct)
        {
            var token = await _authRepository.GetRefreshTokenAsync(request.RefreshToken, ct);
            if (token == null || token.ExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            // Find user by ID instead of email
            var user = await _authRepository.FindByIdAsync(token.UserId, ct);
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            // Generate new tokens
            var accessToken = _tokenService.CreateAccessToken(user, out var accessExpires);
            var newRefreshToken = _tokenService.CreateRefreshToken(out var refreshExpires);

            // Rotate refresh token
            await _authRepository.RevokeRefreshTokenAsync(request.RefreshToken, ct);
            await _authRepository.SaveRefreshTokenAsync(new RefreshToken(
                user.UserId,          
                newRefreshToken,
                refreshExpires
            ), ct);

            return new AuthResult
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiresAt = accessExpires,
                RefreshTokenExpiresAt = refreshExpires
            };
        }

    }

}
