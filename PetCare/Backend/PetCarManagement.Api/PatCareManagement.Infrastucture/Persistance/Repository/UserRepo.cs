using Microsoft.EntityFrameworkCore;
using PatCareManagement.Domain.Entity;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Infrastucture.Persistance.Repository
{
    public class UserRepo : IGenericRepo<User>
    {
        private readonly DbContext dbContext;

        public UserRepo(DbContext dbContext) {
            this.dbContext = dbContext;
        }
        public Task<User> AddAsync(User entity)
        {
            dbContext.Add(entity);
            return  Task.FromResult(entity);
        }
    }
}
