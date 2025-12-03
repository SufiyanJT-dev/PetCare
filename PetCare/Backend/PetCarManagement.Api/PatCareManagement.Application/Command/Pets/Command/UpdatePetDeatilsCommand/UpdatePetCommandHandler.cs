using MediatR;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Pets.Command.UpdatePetDeatilsCommand
{
    public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, int>
    {
        private readonly IGenericRepo<Domain.Entity.Pets> _petRepository;

        public UpdatePetCommandHandler(IGenericRepo<Domain.Entity.Pets> petRepository)
        {
            this._petRepository = petRepository;
        }
        async Task<int> IRequestHandler<UpdatePetCommand, int>.Handle(UpdatePetCommand request, CancellationToken cancellationToken)
        {
            var existingPet = await _petRepository.GetByIdAsync(request.PetId);
            if (existingPet == null)
            {
                throw new Exception("Pet not found");
            }

            existingPet.UpdateProfile(
                name: request.Name,
                species: request.Species,
                breed: request.Breed,
                dob: request.DateOfBirth
               
            );

            await _petRepository.UpdateAsync(existingPet);
            return existingPet.PetId;
        }
    }
}

