using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PatCareManagement.Infrastucture.Persistance.Data;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Infrastucture.Persistance.Repository
{
    public class AuthRepository : IAuth
    {
        private readonly PetCareDbContext _db;
        public AuthRepository(PetCareDbContext db) => _db = db;

        public Task<User?> FindByEmailAsync(string email, CancellationToken ct = default) =>
            _db.Users.SingleOrDefaultAsync(u => u.Email == email, ct);

        public async Task SaveRefreshTokenAsync(RefreshToken token, CancellationToken ct = default)
        {
            _db._refreshTokens.Add(token);
            await _db.SaveChangesAsync(ct);
        }

        public Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct = default) =>
            _db._refreshTokens.SingleOrDefaultAsync(t => t.Token == token, ct);

        public async Task RevokeRefreshTokenAsync(string token, CancellationToken ct = default)
        {
            var rt = await _db._refreshTokens.SingleOrDefaultAsync(t => t.Token == token, ct);
            if (rt == null) return;
            rt.Revoke();
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateUserAsync(User user, CancellationToken ct = default)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync(ct);
        }
    }
   
  
}
