using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.IRepository
{
    public interface IUserRepository<T> where T : class
    {
        Task<T?> AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
    }
}
