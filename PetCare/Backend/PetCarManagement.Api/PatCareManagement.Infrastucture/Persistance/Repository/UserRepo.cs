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
    public class UserRepo : IUserRepository<User>
    {
        private readonly PetCareDbContext dbContext;

        public UserRepo(PetCareDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<User> AddAsync(User entity)
        {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public Task<User?> GetByIdAsync(int id)
        {
            User? user = dbContext.Users.FirstOrDefault(u => u.UserId == id );
            if (user != null)
            {
                return Task.FromResult<User?>(user);
            }
            return Task.FromResult<User?>(null);
        }
    }
}
