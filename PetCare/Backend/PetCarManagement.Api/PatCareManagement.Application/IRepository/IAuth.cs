using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using PetCareManagement.Domain.Entity;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.IRepository
{
    
    public interface IAuth
    {
        Task<Domain.Entity.User?> FindByEmailAsync(string email, CancellationToken ct = default);
        Task SaveRefreshTokenAsync(RefreshToken token, CancellationToken ct = default);
        Task<Domain.Entity.User?> FindByIdAsync(int email, CancellationToken ct = default);
        
        Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct = default);
        Task RevokeRefreshTokenAsync(string token, CancellationToken ct = default);
        Task UpdateUserAsync(Domain.Entity.User user, CancellationToken ct = default);
    }

}
