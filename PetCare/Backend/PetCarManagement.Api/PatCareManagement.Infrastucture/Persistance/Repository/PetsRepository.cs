using Microsoft.EntityFrameworkCore;
using PatCareManagement.Infrastucture.Persistance.Data;
using PetCareManagement.Application.IRepository;

using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Infrastucture.Persistance.Repository
{
    public class PetsRepository : IGenericRepo<Domain.Entity.Pets>, IPetRepository
    {
        private readonly PetCareDbContext petCareDbContext;

        public PetsRepository(PetCareDbContext petCareDbContext)
        {
            this.petCareDbContext = petCareDbContext;
        }
        public async Task<Pets?> AddAsync(Pets entity)
        {
            await petCareDbContext.AddAsync(entity);
            await petCareDbContext.SaveChangesAsync();
            return await Task.FromResult<Pets?>(entity);
        }
      
        public Task<bool> DeleteAsync(int id)
        {
           Pets? pet = petCareDbContext.Pets.FirstOrDefault(p => p.PetId == id);
            if (pet != null)
            {
                pet.Deactivate();
                petCareDbContext.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<IEnumerable<Pets>> FindAsync(Expression<Func<Pets, bool>> predicate)
        {
            IEnumerable<Domain.Entity.Pets> pets=await petCareDbContext.Pets.Where(predicate).ToListAsync();
            return  pets;
        }

        public Task<IEnumerable<Pets>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Pets?> GetByIdAsync(int id)
        {
            Pets? pet = petCareDbContext.Pets.FirstOrDefault(p => p.PetId == id && p.IsActive==true);
            if (pet != null)
            {
              return Task.FromResult<Pets?>(pet);
            }    
            return Task.FromResult<Pets?>(null);
        }

        public async Task<List<Domain.Entity.Pets>> GetPetByOwnerIdAsync(int ownerId)
        {
            List<Pets> pet = await petCareDbContext.Pets.Where(p => p.UserId == ownerId && p.IsActive == true).ToListAsync();
            return pet;
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Pets?> UpdateAsync(Pets entity)
        {
            Domain.Entity.Pets? existingPet = await petCareDbContext.Pets
       .FirstOrDefaultAsync(p => p.PetId == entity.PetId);
            if (existingPet == null)
            {
                return null; 
            }
            existingPet.UpdateProfile(
                entity.Name,
        entity.Species,
        entity.Breed,
        entity.DateOfBirth
                );
            
            await petCareDbContext.SaveChangesAsync();
            return await Task.FromResult<Pets?>(existingPet);

        }

      

       
    }
}
