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
    public  class AttachmentRepository: IAttachmentRepo
    {
        private readonly PetCareDbContext _context;
        private readonly DbSet<Domain.Entity.Attachment> _db;

        public AttachmentRepository(PetCareDbContext context)
        {
            _context = context;
            _db = _context.Set<Domain.Entity.Attachment>();
        }
        public async Task<Domain.Entity.Attachment?> AddAsync(Domain.Entity.Attachment entity)
        {
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Domain.Entity.Attachment entity = await _db.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _db.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Domain.Entity.Attachment>> FindAsync(Expression<Func<Domain.Entity.Attachment, bool>> predicate)
        {
            return await _db.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Domain.Entity.Attachment>> GetAllAsync()
        {
            return await _db.ToListAsync();
        }

        public async Task<IEnumerable<Attachment>> GetAllAttachmentsByPetId(int petId)
        {
            return await _db
       .Include(a => a.MedicalEvent) 
       .Where(a => a.MedicalEvent.PetId == petId)
       .ToListAsync();
        }

        public async Task<Domain.Entity.Attachment?> GetByIdAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Domain.Entity.Attachment?> UpdateAsync(Domain.Entity.Attachment entity)
        {
            _db.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
