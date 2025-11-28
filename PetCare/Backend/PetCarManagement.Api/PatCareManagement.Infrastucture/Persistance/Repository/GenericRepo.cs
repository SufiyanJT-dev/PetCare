using Microsoft.EntityFrameworkCore;
using PatCareManagement.Infrastucture.Persistance.Data;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Infrastucture.Persistance.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly PetCareDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRepo(PetCareDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public async Task<T?> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _db.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T?> UpdateAsync(T entity)
        {
            _db.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
