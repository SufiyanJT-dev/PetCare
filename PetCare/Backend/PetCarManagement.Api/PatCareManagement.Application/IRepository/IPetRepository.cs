using PetCareManagement.Application.Pets.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.IRepository
{
    public interface IPetRepository
    {
        public Task<List<Domain.Entity.Pets>> GetPetByOwnerIdAsync(int ownerId);

    }
}
