using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.IRepository
{
    public interface IGenericRepo<T> where T : class
    {
   
        Task<T?> AddAsync(T entity);
      
      Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

    }
}
