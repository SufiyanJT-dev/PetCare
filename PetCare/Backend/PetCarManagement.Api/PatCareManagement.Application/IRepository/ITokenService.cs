using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.IRepository
{
    public interface ITokenService
    {

        string CreateAccessToken(Domain.Entity.User user, out DateTime expiresAt);
        string CreateRefreshToken(out DateTime expiresAt);
    }
}
