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
    public class UserRepo : IGenericRepo<User>
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
    }
}
