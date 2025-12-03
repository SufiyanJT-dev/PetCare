using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using PetCareManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PetCareManagement.Application.Command.Pets.Command.AddPetComand
{
    public class AddPetComandHandler : IRequestHandler<AddPetComand, int>
    {
        private readonly IGenericRepo<Domain.Entity.Pets> genericRepo;

        public AddPetComandHandler(IGenericRepo<Domain.Entity.Pets> genericRepo) {
            this.genericRepo = genericRepo;
        }
        public async Task<int> Handle(AddPetComand request, CancellationToken cancellationToken)
        {
            Domain.Entity.Pets pets = new Domain.Entity.Pets(
                 userId:request.OwnerId,
                 name:request.Name,
                  species: request.Species,
                  breed: request.Breed,
                    dateOfBirth: request.DateOfBirth
                   
                );
            await genericRepo.AddAsync(pets);
            return pets.PetId;
        }
    }
}
